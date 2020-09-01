using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace control_in_out
{
    public partial class Form1 : Form
    {
        [DllImport("plcommpro.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int Connect(string s);

        // To search devices
        [DllImport("plcommpro.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SearchDevice(string p1, string p2, ref byte p3);


        // To add users
        //  handle
        //      [in]: The handle that is returned when the connection is successful.
        //  TableName
        //      [in]: Data table name. Attached table 4 lists the available data tables.
        //  Data
        //      [in]: Data record; the data is expressed in a text format; the multiple records are separated by \r\n, and the “Field=Value” pairs are separated by \t.
        //  Options
        //      [in]: The default value is null; it is used for extension.
        //  [Returned value]
        //      When the returned value is 0, it indicates that the operation is successful. When the returned value is a negative value, 
        //      it indicates that the operation fails. Attached table 5 lists the information about the error codes.

        [DllImport("plcommpro.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetDeviceData(int handle, string TableName, string Data, string Options);
        
        static int result;

        public Form1()
        {
            InitializeComponent();
            result = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           result = Connect("protocol=TCP,ipaddress=" + tb_device_ip.Text + ",port=4370,timeout=2000,passwd=");
           if (result > 0)
           {
               SetDeviceData(result, "timezone",
               "TimezoneId=1\tSunTime1=2359\tMonTime1=2359\tTueTime1=2359\tWedTime1=2359\tThuTime1=2359\tFriTime1=2359\tSatTime1=2359", "");
               p_main.Enabled = true;
           }
           else MessageBox.Show("Connect to device fail!");
           
        }

        // To add card
        private void button2_Click(object sender, EventArgs e)
        {
            int ret = -1;
            string data = "Pin=" + tb_id.Text + "\tCardNo=" + tb_device_ip.Text + "\tPassword=1";

            ret = SetDeviceData(result,"user",data,"");
            if (ret == 0)
                MessageBox.Show("Add card to user Success!");
            else
                MessageBox.Show("Add card to user Fail!");
            data = "Pin=" + tb_id.Text + "\tAuthorizeTimezoneId=1\tAuthorizeDoorId=3";

            
            ret = SetDeviceData(result, "userauthorize", data, "");
            if (ret == 0)
                MessageBox.Show("Add user to userauthorize Success!");
            else
                MessageBox.Show("Add user to userauthorize Fail!");

            
        }

        
    }
}
