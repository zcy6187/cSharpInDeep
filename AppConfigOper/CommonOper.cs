using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AppConfigOper
{
    /* 系统内置的App.Config的读写方法,可以读写AppSettings/ConnectionStrings等结点的信息
     * App.Config中的内容是预存在缓存中的，
     * 修改的方法是针对App.Config文件的，但读取的方法却是针对内存中的缓存内容，所以如果对内容进行了修改，一定要进行RefreshSection。
     */
    class CommonOper
    {
        /// <summary>
        /// 读取结点信息
        /// </summary>
        public static void ReadConfigNode()
        {
            //读取AppSettings中的数据
            string str = System.Configuration.ConfigurationManager.AppSettings["ServerIp"];
            //读取ConnectionStrings中的数据
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["123"].ConnectionString;
            Console.WriteLine("AppSettings['ServerIp']" + str);
            Console.WriteLine("ConnectionStrings['123']" + connStr);
        }


        /// <summary>
        /// 修改结点信息
        /// </summary>
        public static void WriteConfigNode()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ServerIp"].Value = "[{\"xxhz\":\"20英尺\",\"ysxq\":{\"PK\":\"\",\"bureauSourceId\":\"O00\",\"bureauTargetId\":\"#00\",\"dj\":\"郑州局\",\"djdm\":\"F00\",\"dxhxzz\":\"\",\"dxpdfs\":\"1\",\"dz\":\"盘古寺\",\"dzlm\":\"PGF\",\"dztmism\":\"28769\",\"dzyxbm\":\"28769003\",\"dzyxmc\":\"济源市兴达煤炭运销有限公司专用线\",\"expfile\":\"\",\"exptime\":\"\",\"fffs\":\"\",\"ffh\":\"\",\"fhdwdz\":\"格尔木市江源小区\",\"fhjbrsfzhm\":\"\",\"fhjbrsj\":\"\",\"fhjbrxm\":\"\",\"fhr\":\"解军-410827197203080779\",\"fhrbm\":\"3901851\",\"fhrdh\":\"13519798225\",\"fj\":\"青藏公司\",\"fjdm\":\"O00\",\"flag\":\"Y\",\"fplx\":\"\",\"fz\":\"格尔木\",\"fzlm\":\"GRO\",\"fztmism\":\"43787\",\"fzyxbm\":\"43787000\",\"fzyxmc\":\"铁路货场\",\"glptshdwdmYd\":\"\",\"glptshdwmcYd\":\"\",\"hsxs\":0,\"ifcc\":\"\",\"ifdzlh\":\"\",\"ifll\":\"\",\"ifzzjg\":\"\",\"jzrq\":\"2017-12-16\",\"khbm\":\"3901851\",\"lastModifyTime\":{\"date\":14,\"day\":4,\"hours\":17,\"minutes\":48,\"month\":11,\"nanos\":0,\"seconds\":40,\"time\":1513244920000,\"timezoneOffset\":-480,\"year\":117},\"lastModifyUser\":\"\",\"lastSyncTime\":null,\"loadfile\":\"\",\"loadtime\":\"\",\"nsrdh\":\"\",\"nsrdz\":\"\",\"nsrkhh\":\"\",\"nsrsbh\":\"\",\"nsrzh\":\"\",\"pm\":\"氯化镁\",\"pmbm\":\"1591008\",\"qsrq\":\"2017-12-16\",\"shdwdz\":\"\",\"shhr\":\"\",\"shhrbm\":\"\",\"shhsj\":\"\",\"shjbrsfzhm\":\"\",\"shjbrsj\":\"\",\"shjbrxm\":\"\",\"shrbm\":\"3109087\",\"shrdh\":\"13897051665\",\"shrr\":\"河南坤鑫物流有限公司\",\"shxs\":0,\"spfmc\":\"\",\"sskhdm\":\"3901851\",\"sskhmc\":\"解军\",\"ssqydm\":\"3901851\",\"ssqymc\":\"解军-410827197203080779\",\"stateLevel\":\"\",\"submitStatus\":false,\"sxxs\":0,\"tbfs\":\"211\",\"tbqydm\":\"3901851\",\"tbqymc\":\"解军-410827197203080779\",\"transtate\":\"\",\"txfs\":\"Z131\",\"txz\":\"格尔木\",\"txzlm\":\"GRO\",\"type\":\"\",\"tyrjzsx\":\"\",\"tyrthmm\":\"\",\"uuid\":\"604A2A65772E035AE0530AD8081B74C0\",\"xl\":\"通用标准箱\",\"xlbm\":\"GJ\",\"xx\":\"20\",\"xxxs\":0,\"ytxs\":0,\"ytydxs\":0,\"yyds\":0,\"yyh\":\"DO1712140057\",\"yyid\":\"GRO201712140037\",\"yyxxstr\":\"\",\"yyzt\":\"YY03\",\"zmlm\":\"GRO\",\"zxs\":2,\"zxxs\":0,\"zyr\":\"解军\",\"zyrbm\":\"3901851\",\"zysj\":\"2017-12-14\"},\"sjqa\":\"氯化镁\",\"dcxxstr\":\"\"}]";
            config.ConnectionStrings.ConnectionStrings["123"].ConnectionString = "test123";
            //一定要保存
            //ConfigurationSaveMode有三个属性：Full、Minimal、Modified，
            //Full 会补全一些必须信息；
            //Modified 只保存修改的信息，一般用这个；
            //Minimal  不同于继承值的属性写入到文件中
            config.Save(ConfigurationSaveMode.Modified);

            //刷新缓存的配置文件,再读取的内容就会是最新值，否则修改的内容不会生效，除非软件重启
            //结点名字区分大小写
            ConfigurationManager.RefreshSection("connectionStrings");
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 添加结点ConnectionStrings
        /// </summary>
        public static void AddConfigNode()
        {
            ConnectionStringSettings mySettings =
       new ConnectionStringSettings("newName", "newConString", "newProviderName");
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Add(mySettings);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public static void ReadSelfDefNode()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            IDictionary dic = ConfigurationManager.GetSection("MySectionGroup/MySecondSection") as IDictionary;
            string tempStr = dic["Second"].ToString();

            Console.WriteLine(tempStr);
        }
    }
}
