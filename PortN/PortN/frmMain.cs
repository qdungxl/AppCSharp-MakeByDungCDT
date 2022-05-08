using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace PortN
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        string[] ListBaundrate = { "110", "300", "600", "1200", "4800", "9600", "14400", "19200", "38400", "57600", "115200", "128000", "256000" };
        string Mess1 = "";
        string Mess2 = "";

        private void frmMain_Load(object sender, EventArgs e)
        {
            string[] ListNameCom = SerialPort.GetPortNames();
            cboCOM1.Items.AddRange(ListNameCom);
            cboBaundRate1.Items.AddRange(ListBaundrate);
            cboBaundRate1.SelectedIndex = 5;
            cboCOM2.Items.AddRange(ListNameCom);
            cboBaundRate2.Items.AddRange(ListBaundrate);
            cboBaundRate2.SelectedIndex = 5;
        }

        private void btnConnect1_Click(object sender, EventArgs e)
        {
            if(cboCOM1.Text == string.Empty)
            {
                MessageBox.Show("Hãy chọn cổng COM.");
            }
            else
            {
                if(serialPort1.IsOpen == true)
                {        
                    serialPort1.Close();
                    serialPort1.Dispose();
                    btnConnect1.Text = "Connect";
                }
                else if (serialPort1.IsOpen == false)
                {
                    try
                    {                        
                        serialPort1.PortName = cboCOM1.Text;
                        serialPort1.BaudRate = int.Parse(cboBaundRate1.Text);
                        serialPort1.Open();
                        btnConnect1.Text = "Disconnect";               
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           if(serialPort1.IsOpen)
            {
                lblState1.Text = "OnLine";
                lblState1.BackColor = Color.LightGreen;
            }
           else if (serialPort1.IsOpen == false)
            {
                lblState1.Text = "OffLine";
                lblState1.BackColor = Color.Orange;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                lblState2.Text = "OnLine";
                lblState2.BackColor = Color.LightGreen;
            }
            else if (serialPort2.IsOpen == false)
            {
                lblState2.Text = "OffLine";
                lblState2.BackColor = Color.Orange;
            }
        }

        private void btnXoa1_Click(object sender, EventArgs e)
        {
            txtRecieve1.Clear();
        }

        private void btnXoa2_Click(object sender, EventArgs e)
        {
            txtRecieve2.Clear();
        }

        private void btnReset1_Click(object sender, EventArgs e)
        {
            string[] ListNameCom = SerialPort.GetPortNames();
            cboCOM1.Items.Clear();
            cboCOM1.Items.AddRange(ListNameCom);
        }

        private void btnReset2_Click(object sender, EventArgs e)
        {
            string[] ListNameCom = SerialPort.GetPortNames();
            cboCOM2.Items.Clear();
            cboCOM2.Items.AddRange(ListNameCom);
        }

        private void btnSend1_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine(txtSend1.Text);
            txtSend1.Text = "";
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string DataReceive = serialPort1.ReadLine().ToString();
            Mess1 = DataReceive + "\n" + Mess1;
            try
            {
                BeginInvoke(new Action(() =>
                {
                    txtRecieve1.Text = Mess1;
                }
                ));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConnect2_Click(object sender, EventArgs e)
        {
            if (cboCOM2.Text == string.Empty)
            {
                MessageBox.Show("Hãy chọn cổng COM.");
            }
            else
            {
                if (serialPort2.IsOpen == true)
                {
                    serialPort2.Close();
                    serialPort2.Dispose();
                    btnConnect2.Text = "Connect";
                }
                else if (serialPort2.IsOpen == false)
                {
                    try
                    {
                        serialPort2.PortName = cboCOM2.Text;
                        serialPort2.BaudRate = int.Parse(cboBaundRate2.Text);
                        serialPort2.Open();
                        btnConnect2.Text = "Disconnect";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnSend2_Click(object sender, EventArgs e)
        {
            serialPort2.WriteLine(txtSend2.Text);
            txtSend2.Text = "";
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string DataReceive = serialPort2.ReadLine().ToString();
            Mess2 = DataReceive + "\n" + Mess2;
            try
            {
                BeginInvoke(new Action(() =>
                {
                    txtRecieve2.Text = Mess2;
                }
                ));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
