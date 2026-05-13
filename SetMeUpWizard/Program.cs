using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace RdpShortcutCreator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new ShortcutCreatorForm());
        }
    }

    public class ShortcutCreatorForm : Form
    {
        private Label labelDesktop;
        private Label labelExe;
        private Label labelRdp;
        private static Label labelName;
        private static TextBox textBoxName;
        private TextBox textBoxDesktop;
        private TextBox textBoxExe;
        private TextBox textBoxRdp;
        private Button buttonBrowseDesktop;
        private Button buttonBrowseExe;
        private Button buttonBrowseRdp;
        private static Button buttonCreate;
        private static Label statusLabel;
        private Label labelOpts;

        public ShortcutCreatorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "RDP Shortcut Creator";
            this.Width = 600;
            this.Height = 620;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Desktop Label and Controls
            labelDesktop = new Label();
            labelDesktop.Text = "Desktop Location:";
            labelDesktop.Location = new System.Drawing.Point(20, 20);
            labelDesktop.Size = new System.Drawing.Size(120, 23);
            this.Controls.Add(labelDesktop);

            textBoxDesktop = new TextBox();
            textBoxDesktop.ReadOnly = true;
            textBoxDesktop.Location = new System.Drawing.Point(20, 45);
            textBoxDesktop.Size = new System.Drawing.Size(400, 23);
            this.Controls.Add(textBoxDesktop);

            buttonBrowseDesktop = new Button();
            buttonBrowseDesktop.Text = "Browse...";
            buttonBrowseDesktop.Location = new System.Drawing.Point(430, 45);
            buttonBrowseDesktop.Size = new System.Drawing.Size(130, 23);
            buttonBrowseDesktop.Click += ButtonBrowseDesktop_Click;
            this.Controls.Add(buttonBrowseDesktop);

            // EXE Label and Controls
            labelExe = new Label();
            labelExe.Text = "RdpAutoClick.exe:";
            labelExe.Location = new System.Drawing.Point(20, 80);
            labelExe.Size = new System.Drawing.Size(120, 23);
            this.Controls.Add(labelExe);

            textBoxExe = new TextBox();
            textBoxExe.ReadOnly = true;
            textBoxExe.Location = new System.Drawing.Point(20, 105);
            textBoxExe.Size = new System.Drawing.Size(400, 23);
            this.Controls.Add(textBoxExe);

            buttonBrowseExe = new Button();
            buttonBrowseExe.Text = "Browse...";
            buttonBrowseExe.Location = new System.Drawing.Point(430, 105);
            buttonBrowseExe.Size = new System.Drawing.Size(130, 23);
            buttonBrowseExe.Click += ButtonBrowseExe_Click;
            this.Controls.Add(buttonBrowseExe);


            
            // RDP Label and Controls
            labelRdp = new Label();
            labelRdp.Text = ".RDP File:";
            labelRdp.Location = new System.Drawing.Point(20, 140);
            labelRdp.Size = new System.Drawing.Size(120, 23);
            this.Controls.Add(labelRdp);

            textBoxRdp = new TextBox();
            textBoxRdp.ReadOnly = true;
            textBoxRdp.Location = new System.Drawing.Point(20, 165);
            textBoxRdp.Size = new System.Drawing.Size(400, 23);
            this.Controls.Add(textBoxRdp);

            buttonBrowseRdp = new Button();
            buttonBrowseRdp.Text = "Browse...";
            buttonBrowseRdp.Location = new System.Drawing.Point(430, 165);
            buttonBrowseRdp.Size = new System.Drawing.Size(130, 23);
            buttonBrowseRdp.Click += ButtonBrowseRdp_Click;
            this.Controls.Add(buttonBrowseRdp);


            // opts Label and Controls
            labelOpts = new Label();
            labelOpts.Text = "Options:";
            labelOpts.Location = new System.Drawing.Point(20, 200);
            labelOpts.Size = new System.Drawing.Size(120, 23);
            this.Controls.Add(labelOpts);

            checkedList = new CheckedListBox();
            checkedList.Enabled = false;
            checkedList.Location = new System.Drawing.Point(20, 225);
            checkedList.Size = new System.Drawing.Size(400, 23);
            checkedList.Items.Add("-all");
            checkedList.ItemCheck += CheckedList_ItemCheck;
            optionsSelected = new Dictionary<string, bool>();
            optionsSelected["-all"] = false;

            this.Controls.Add(checkedList);

            buttonBrowseRdp = new Button();
            //buttonBrowseRdp.Text = "Browse...";
            //buttonBrowseRdp.Location = new System.Drawing.Point(430, 165);
            //buttonBrowseRdp.Size = new System.Drawing.Size(130, 23);
            //buttonBrowseRdp.Click += ButtonBrowseRdp_Click;
            //this.Controls.Add(buttonBrowseRdp);
            // Shortcut name
            labelName = new Label();
            labelName.Text = "Shortcut Name:";
            labelName.Location = new System.Drawing.Point(20, 260);
            labelName.Size = new System.Drawing.Size(120, 23);
            this.Controls.Add(labelName);

            textBoxName = new TextBox();
            textBoxName.ReadOnly = false;
            textBoxName.Location = new System.Drawing.Point(20, 285);
            textBoxName.Size = new System.Drawing.Size(400, 23);
            this.Controls.Add(textBoxName);

            //buttonBrowseExe = new Button();
            //buttonBrowseExe.Text = "Browse...";
            //buttonBrowseExe.Location = new System.Drawing.Point(430, 105);
            //buttonBrowseExe.Size = new System.Drawing.Size(130, 23);
            //buttonBrowseExe.Click += ButtonBrowseExe_Click;
            //this.Controls.Add(buttonBrowseExe);






            // Create Button
            buttonCreate = new Button();
            buttonCreate.Text = "Create Shortcut";
            buttonCreate.Location = new System.Drawing.Point(20, 360);
            buttonCreate.Size = new System.Drawing.Size(130, 35);
            buttonCreate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            buttonCreate.Click += ButtonCreate_Click;
            this.Controls.Add(buttonCreate);

            // Status Label
            statusLabel = new Label();
            statusLabel.Text = "Ready...";
            statusLabel.Location = new System.Drawing.Point(160, 360+yOffset);
            statusLabel.Size = new System.Drawing.Size(400, 35);
            statusLabel.AutoSize = false;
            statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Controls.Add(statusLabel);
        }
        private static  Dictionary<string, bool> optionsSelected;
        private void CheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string currentItem = checkedList.Items[e.Index].ToString();

            bool currentChecked = e.NewValue == CheckState.Checked;

            // If "-all" is being checked
            if (currentItem == "-all")
            {
                for (int i = 1; i < checkedList.Items.Count; i++)
                {
                    checkedList.SetItemChecked(i, !currentChecked);
                }
                optionsSelected[currentItem] = true;
            }

            Match match = Regex.Match(currentItem, @"\(Id:(.*?)\)");

            if (match.Success)
            {
                string id = match.Groups[1].Value;
                //Console.WriteLine(id);
                optionsSelected[id] = currentChecked;
            }
        }

        private void ButtonBrowseDesktop_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select Desktop Location";
                dialog.RootFolder = Environment.SpecialFolder.UserProfile;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxDesktop.Text = dialog.SelectedPath;
                    statusLabel.Text = "Desktop path selected.";
                }
            }
        }

        private void ButtonBrowseExe_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select RdpAutoClick.exe";
                dialog.Filter = "RdpAutoClick|RdpAutoClick.exe|EXE Files|*.exe|All Files|*.*";
                dialog.DefaultExt = ".exe";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxExe.Text = dialog.FileName;
                    statusLabel.Text = "EXE file selected.";
                }
            }
        }
        private static void ProcessAllAvailableCheckboxes(string rdpPath)

        {
            LaunchRdp(rdpPath);
            AutomationElement window = WaitForWindow("Remote Desktop Connection security warning", 15000);

            if (window == null)
            {
                Message("RDP window not found");
            }

            //var winow = 
            var checkboxes = window.FindAll(
                                    TreeScope.Descendants,
                                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.CheckBox)
                                );

            var report = ($"Found {checkboxes.Count} checkboxes:\n");
            List<string> allNames = new List<string>();
            List<string> all = new List<string>();
            int cnt = 1;
            foreach (AutomationElement cb in checkboxes)
            {
                all.Add(cb.Current.AutomationId);
                allNames.Add(cb.Current.Name);
                report += Environment.NewLine + Environment.NewLine + cnt.ToString() + Environment.NewLine + ($"Name: {cb.Current.Name}") +
                Environment.NewLine + ($"AutomationId: {cb.Current.AutomationId}") +
                Environment.NewLine + ("-------------------------");
                cnt++;
            }

            try
            {
                var windowPattern = window.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
                windowPattern?.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            //string path = rdpPath;

            string fileName = Path.GetFileName(rdpPath);
            textBoxName.Text = fileName;

            ActivatOptionPicker(allNames,all);

        }

        private static  CheckedListBox checkedList;
        private static List<string> optionIds;
        private static void ActivatOptionPicker(List<string> allNames, List<string> all)
        {
            checkedList.Enabled = true;
            int cnt = 0;
            var options = new List<string>();
            optionIds = new List<string>();
            foreach (var nm in allNames)
            {
                var opt = $"{nm} + (Id: {all[cnt]})";
                optionIds.Add(all[cnt]);
                optionsSelected[all[cnt]] = false;
                options.Add(opt);
                cnt++;
            }

            foreach(var opt in options)
            {
                checkedList.Items.Add(opt);
            }

            var newY = (options.Count+1) * 23;
            yOffset = newY;
            checkedList.Size = new System.Drawing.Size(checkedList.Size.Width, newY);
            buttonCreate.Location = new System.Drawing.Point(20, 300 + yOffset);


            statusLabel.Location = new System.Drawing.Point(160, 210 + yOffset); 

            labelName.Location = new System.Drawing.Point(20, 240 + yOffset);
            textBoxName.Location = new System.Drawing.Point(20, 265 + yOffset);
        }

        private static int yOffset = 0;
        static void LaunchRdp(string path)
        {
            ShellExecute(IntPtr.Zero, "open", "mstsc.exe", "\"" + path + "\"", null, 1);
        }

        static AutomationElement WaitForWindow(string title, int timeout)
        {
            int elapsed = 0;

            while (elapsed < timeout)
            {
                AutomationElement root = AutomationElement.RootElement;

                AutomationElement win = root.FindFirst(
                    TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty, title));

                if (win != null)
                {

                    Thread.Sleep(150);
                    return win;
                }

                Thread.Sleep(200);
                elapsed += 200;
            }

            return null;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ShellExecute(
     IntPtr hwnd,
     string lpOperation,
     string lpFile,
     string lpParameters,
     string lpDirectory,
     int nShowCmd);

        // Win32: message box
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

        const uint MB_OK = 0x00000000;
        const uint MB_ICONERROR = 0x00000010;
        const uint MB_ICONWARNING = 0x00000030;
        const uint MB_ICONINFORMATION = 0x00000040;

        static void Message(string msg, uint type = MB_ICONERROR)
        {
            MessageBoxW(IntPtr.Zero, msg, "RDP AutoClick", MB_OK | type);
        }
        private void ButtonBrowseRdp_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select RDP File";
                dialog.Filter = "RDP Files|*.rdp|All Files|*.*";
                dialog.DefaultExt = ".rdp";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxRdp.Text = dialog.FileName;
                    statusLabel.Text = "RDP file selected.";
                    ProcessAllAvailableCheckboxes(dialog.FileName);
                }
            }
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(textBoxDesktop.Text))
            {
                MessageBox.Show("Please select a desktop location.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxExe.Text))
            {
                MessageBox.Show("Please select the RdpAutoClick.exe file.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxRdp.Text))
            {
                MessageBox.Show("Please select an RDP file.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(textBoxDesktop.Text))
            {
                MessageBox.Show("Desktop location does not exist.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(textBoxExe.Text))
            {
                MessageBox.Show("RdpAutoClick.exe file does not exist.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(textBoxRdp.Text))
            {
                MessageBox.Show("RDP file does not exist.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Create shortcut
                CreateShortcut(
                    textBoxDesktop.Text,
                    textBoxExe.Text,
                    textBoxRdp.Text,optionsSelected
                );

                statusLabel.Text = "Shortcut created successfully!";
                MessageBox.Show("Shortcut created successfully on the desktop!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Error creating shortcut.";
                MessageBox.Show($"Error creating shortcut: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateShortcut(string desktopPath, string exePath, string rdpPath, Dictionary<string, bool> optionsSelected)
        {
            // Get the RDP filename without extension for the shortcut name
            //  string rdpFileName = Path.GetFileNameWithoutExtension(rdpPath);
            string name = textBoxName.Text;
            string shortcutPath = Path.Combine(desktopPath, name + ".lnk");

            // Use Windows Script Host to create the shortcut
            dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
            dynamic shortcut = shell.CreateShortcut(shortcutPath);

            shortcut.TargetPath = exePath;
            string args = "";
            if (optionsSelected["-all"])
                args = "-all";
            else
            {
                foreach(var opt in optionsSelected)
                {
                    if (opt.Value)
                        args += " " + opt.Key; 

                }
            }
                shortcut.Arguments = "\"" + rdpPath + "\""+ " "+args;
            shortcut.WorkingDirectory = Path.GetDirectoryName(exePath);
            shortcut.Description = "RDP Connection: " + name;

            shortcut.IconLocation = @"C:\Windows\System32\mstsc.exe,0";
            shortcut.Save();
            Marshal.FinalReleaseComObject(shortcut);
            Marshal.FinalReleaseComObject(shell);
        }
    }
}