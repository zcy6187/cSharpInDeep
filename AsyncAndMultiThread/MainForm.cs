using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsyncAndMultiThread
{
    public partial class MainForm : Form
    {
        delegate double ExeCalucate(double r);
        delegate double ExeCalucateNoPram();
        ExeCalucate ec;
        ExeCalucateNoPram ecp;
        delegate void ChangeText(string r);
        public MainForm()
        {
            InitializeComponent();
            CalCls cc = new CalCls();
            cc.mf = this;
            ec = new ExeCalucate(cc.Calucate);
            ecp = new ExeCalucateNoPram(cc.CalucateByForm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //double r = Convert.ToDouble(this.tbR.Text);
            //ec.BeginInvoke(r, new AsyncCallback(TaskFinished), null);

            ecp.BeginInvoke(new AsyncCallback(TaskFinished), null);
        }

        private void TaskFinished(IAsyncResult iAr)
        {
            /*在线程中直接调用winform UI 会出错，因为WInform的UI线程有严格的要求
             *跨线程访问很难做到
             * 多线程访问Winform控件需要在Winform中定义一个委托，和该委托可对应的方法来实现            
                winform定义的委托，在多线程方法中也得可以访问
             */
            //double da = ec.EndInvoke(iAr);

            double da = ecp.EndInvoke(iAr);
            this.BeginInvoke(new ChangeText(ChangeTextVal), da.ToString());
            //this.tbDiameter.Text = da.ToString();
        }

        private void ChangeTextVal(string val)
        {
            this.tbDiameter.Text = val;
        }



    }

    class CalCls
    {
        public MainForm mf { get; set; }
        public double Calucate(double r)
        {
            double ret = r * Math.PI;
            return ret;
        }

        public double CalucateByForm()
        {
            string ret = (this.mf.Controls.Find("tbR", true)[0] as TextBox).Text;

            double calRet = Convert.ToDouble(ret) * Math.PI;
            return calRet;
        }
    }
}
