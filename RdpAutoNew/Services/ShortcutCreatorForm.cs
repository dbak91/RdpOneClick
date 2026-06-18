using RdpAutoClickNew.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RdpShortcutCreator
{
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

        private Label labelDesktopStatus;
        private Label labelExeStatus;
        private Label labelRdpStatus;

        private static Dictionary<string, bool> optionsSelected;
        private static CheckedListBox checkedList;
        private static List<string> optionIds;

        private static int yOffset = 0;

        public ShortcutCreatorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "RDP Shortcut Creator";
            this.Width = 630;
            this.Height = 620;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Desktop
            labelDesktop = new Label();
            labelDesktop.Text = LanguageService.T("DesktopLabel");
            labelDesktop.Location = new System.Drawing.Point(20, 20);
            labelDesktop.Size = new System.Drawing.Size(200, 23);
            this.Controls.Add(labelDesktop);

            textBoxDesktop = new TextBox();
            textBoxDesktop.ReadOnly = true;
            textBoxDesktop.Location = new System.Drawing.Point(20, 45);
            textBoxDesktop.Size = new System.Drawing.Size(400, 23);
            // Set default Desktop location
            textBoxDesktop.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktopSet = true;
            this.Controls.Add(textBoxDesktop);

            buttonBrowseDesktop = new Button();
            buttonBrowseDesktop.Text = LanguageService.T("Browse");
            buttonBrowseDesktop.Location = new System.Drawing.Point(430, 45);
            buttonBrowseDesktop.Size = new System.Drawing.Size(130, 23);
            buttonBrowseDesktop.Click += ButtonBrowseDesktop_Click;
            this.Controls.Add(buttonBrowseDesktop);

            labelDesktopStatus = new Label();
            labelDesktopStatus.Location = new Point(565, 48);
            labelDesktopStatus.Size = new Size(20, 20);
            this.Controls.Add(labelDesktopStatus);

            // EXE
            labelExe = new Label();
            labelExe.Text = LanguageService.T("RdpAuto");
            labelExe.Location = new System.Drawing.Point(20, 80);
            labelExe.Size = new System.Drawing.Size(200, 23);
            this.Controls.Add(labelExe);

            textBoxExe = new TextBox();
            textBoxExe.ReadOnly = true;
            textBoxExe.Location = new System.Drawing.Point(20, 105);
            textBoxExe.Size = new System.Drawing.Size(400, 23);
            // Set to this app version
            textBoxExe.Text = Application.ExecutablePath;
            exeSet = true;
            this.Controls.Add(textBoxExe);

            buttonBrowseExe = new Button();
            buttonBrowseExe.Text = LanguageService.T("Browse");
            buttonBrowseExe.Location = new System.Drawing.Point(430, 105);
            buttonBrowseExe.Size = new System.Drawing.Size(130, 23);
            buttonBrowseExe.Click += ButtonBrowseExe_Click;
            this.Controls.Add(buttonBrowseExe);

            labelExeStatus = new Label();
            labelExeStatus.Location = new Point(565, 108);
            labelExeStatus.Size = new Size(20, 20);
            this.Controls.Add(labelExeStatus);

            // RDP
            labelRdp = new Label();
            labelRdp.Text = LanguageService.T("TargetRdp"); 
            labelRdp.Location = new System.Drawing.Point(20, 140);
            labelRdp.Size = new System.Drawing.Size(200, 23);
            this.Controls.Add(labelRdp);

            textBoxRdp = new TextBox();
            textBoxRdp.ReadOnly = true;
            textBoxRdp.Location = new System.Drawing.Point(20, 165);
            textBoxRdp.Size = new System.Drawing.Size(400, 23);
            this.Controls.Add(textBoxRdp);

            buttonBrowseRdp = new Button();
            buttonBrowseRdp.Text = LanguageService.T("Browse");
            buttonBrowseRdp.Location = new System.Drawing.Point(430, 165);
            buttonBrowseRdp.Size = new System.Drawing.Size(130, 23);
            buttonBrowseRdp.Click += ButtonBrowseRdp_Click;
            this.Controls.Add(buttonBrowseRdp);

            labelRdpStatus = new Label();
            labelRdpStatus.Location = new Point(565, 168);
            labelRdpStatus.Size = new Size(20, 20);
            this.Controls.Add(labelRdpStatus);

            // OPTIONS
            labelOpts = new Label();
            labelOpts.Text = LanguageService.T("OptionsLabel"); 
            labelOpts.Size = new System.Drawing.Size(300,23);
            labelOpts.Location = new System.Drawing.Point(20, 200);
            this.Controls.Add(labelOpts);

            checkedList = new CheckedListBox();
            checkedList.Enabled = false;
            checkedList.Location = new System.Drawing.Point(20, 225);
            checkedList.Size = new System.Drawing.Size(540, 23);
            checkedList.Items.Add(allItem);
            checkedList.ItemCheck += CheckedList_ItemCheck;
            checkedList.Click += CheckedList_Click;

            optionsSelected = new Dictionary<string, bool>();
            optionsSelected["-all"] = false;

            optionIds = new List<string>();

            this.Controls.Add(checkedList);

            // NAME
            labelName = new Label();
            labelName.Text = LanguageService.T("ShortcutName");
            labelName.Location = new System.Drawing.Point(20, 260);
            labelName.Size = new System.Drawing.Size(200, 23);
            this.Controls.Add(labelName);

            textBoxName = new TextBox();
            textBoxName.Location = new System.Drawing.Point(20, 285);
            textBoxName.Size = new System.Drawing.Size(540, 23);
            this.Controls.Add(textBoxName);

            // CREATE
            buttonCreate = new Button();
            buttonCreate.Text = LanguageService.T("Create"); 
            buttonCreate.Location = new System.Drawing.Point(20, 360);
            buttonCreate.Size = new System.Drawing.Size(130, 35);
            buttonCreate.Click += ButtonCreate_Click;
            this.Controls.Add(buttonCreate);

            // STATUS
            statusLabel = new Label();
            statusLabel.Text = LanguageService.T("Configure"); 
            statusLabel.Location = new System.Drawing.Point(200, 365);
            statusLabel.Size = new System.Drawing.Size(400, 35);
            this.Controls.Add(statusLabel);

            //  initial tick or cross
            UpdateStatusLabel(labelDesktopStatus, desktopSet);
            UpdateStatusLabel(labelExeStatus, exeSet);
            UpdateStatusLabel(labelRdpStatus, rdpSet);
        }
        private void UpdateStatusLabel(Label label, bool isValid)
        {
            label.Text = isValid ? "✓" : "✗";
            label.ForeColor = isValid ? Color.Green : Color.Red;
        }
        private bool IsReady()
        {
            return desktopSet && exeSet && rdpSet;
        }
        private void ButtonBrowseDesktop_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = Directory.Exists(textBoxDesktop.Text)
                        ? textBoxDesktop.Text
                        : Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxDesktop.Text = dialog.SelectedPath;
                    desktopSet = true;
                    UpdateStatusLabel(labelDesktopStatus, desktopSet);
                    if (IsReady())
                        statusLabel.Text = LanguageService.T("Ready");
                    else
                        statusLabel.Text = LanguageService.T("Configure");
                }

            }
        }

        private void ButtonBrowseExe_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "EXE Files|*.exe";
                if (File.Exists(textBoxExe.Text))
                {
                    dialog.InitialDirectory = Path.GetDirectoryName(textBoxExe.Text);
                    dialog.FileName = Path.GetFileName(textBoxExe.Text);
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxExe.Text = dialog.FileName;
                    exeSet = true;
                    UpdateStatusLabel(labelExeStatus, exeSet);
                    if (IsReady())
                        statusLabel.Text = LanguageService.T("Ready");
                    else
                        statusLabel.Text = LanguageService.T("Configure");

                }
            }
        }
        private static bool rdpSet =false;
        private static bool desktopSet = false;
        private static bool exeSet = false;
        private void ButtonBrowseRdp_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "RDP Files|*.rdp";


                dialog.InitialDirectory = Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop);


                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    rdpSet = true;
                    textBoxRdp.Text = dialog.FileName;

                    List<string> names;
                    List<string> ids;
                    statusLabel.Text = LanguageService.T("Loading");
                    RdpInteractionService.GetRdpCheckboxData(dialog.FileName, out names, out ids);

                    textBoxName.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
                    UpdateStatusLabel(labelRdpStatus, rdpSet);// update tick or cross

                    
                    ActivatOptionPicker(names, ids);
                    if (IsReady())
                        statusLabel.Text = LanguageService.T("Ready");
                    else
                        statusLabel.Text = LanguageService.T("Configure");
                }
            }
        }
        private readonly string allItem = "-all : " + LanguageService.T("SelectAllDesc");
        private void ActivatOptionPicker(List<string> allNames, List<string> all)
        {
            checkedList.Enabled = true;

            checkedList.Items.Clear();
            checkedList.Items.Add(allItem);
            optionsSelected.Clear();
            optionsSelected["-all"] = false;
            optionIds.Clear();

            List<string> options = new List<string>();

            int cnt = 0;

            foreach (var nm in allNames)
            {
                var opt = $"{nm} - (Id: {all[cnt]})";
                optionIds.Add(all[cnt]);
                optionsSelected[all[cnt]] = false;
                options.Add(opt);
                cnt++;
            }

            foreach (var opt in options)
                checkedList.Items.Add(opt);

            yOffset = Math.Min((options.Count + 1) * 23,240);

            checkedList.Size = new System.Drawing.Size(400, yOffset);
            statusLabel.Location = new System.Drawing.Point(200, 305 + yOffset);
            buttonCreate.Location = new System.Drawing.Point(20, 300 + yOffset);
            labelName.Location = new System.Drawing.Point(20, 240 + yOffset);
            textBoxName.Location = new System.Drawing.Point(20, 265 + yOffset);
        }

        private static bool programmableChange =false;

        private void CheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string currentItem = checkedList.Items[e.Index].ToString();
            bool currentChecked = e.NewValue == CheckState.Checked;

            if (currentItem == allItem)
            {
                optionsSelected["-all"] = currentChecked;

                // DO NOT enable/disable the control here
                return;
            }

            Match match = Regex.Match(currentItem, @"\(Id:(.*?)\)");

            if (match.Success)
            {
                string id = match.Groups[1].Value;
                optionsSelected[id] = currentChecked;
            }
        }
       

         private void CheckedList_Click(object sender, EventArgs e)
        {
            bool newState = !optionsSelected["-all"];
            if (checkedList.SelectedItem?.ToString() == allItem)
            {
                

                for (int i = 1; i < checkedList.Items.Count; i++)
                {
                    if(newState)
                        checkedList.SetItemChecked(i, !newState);
                }

                
                //checkedList.ItemCheck -= CheckedList_ItemCheck;
               // checkedList.SetItemChecked(0, newState);
                //checkedList.ItemCheck -= CheckedList_ItemCheck;
                optionsSelected["-all"] = newState;
            }
        }
        public const uint MB_ICONWARNING = 0x00000030;
        public const uint MB_ICONERROR = 0x00000010;
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(textBoxDesktop.Text))
            {
                NativeMethods.Message(LanguageService.T("DesktopLocation"),MB_ICONWARNING);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxExe.Text))
            {
                NativeMethods.Message(LanguageService.T("ExeLocation"), MB_ICONWARNING);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxRdp.Text))
            {
                NativeMethods.Message(LanguageService.T("RdpLocation"), MB_ICONWARNING);
                return;
            }

            if (!Directory.Exists(textBoxDesktop.Text))
            {
                NativeMethods.Message(LanguageService.T("DesktopNotExist"),MB_ICONERROR);
                return;
            }

            if (!File.Exists(textBoxExe.Text))
            {
                NativeMethods.Message(LanguageService.T("ExeNotExist"), MB_ICONERROR);
                return;
            }

            if (!File.Exists(textBoxRdp.Text))
            {
                NativeMethods.Message(LanguageService.T("RdpNotExist"), MB_ICONERROR);
                return;
            }

            try
            {
                // Create shortcut
                CreateShortcut(
                    textBoxDesktop.Text,
                    textBoxExe.Text,
                    textBoxRdp.Text, optionsSelected
                );

                //  statusLabel.Text = "Shortcut created successfully!";
                const uint MB_OK = 0x00000000;
                NativeMethods.Message(LanguageService.T("CreatedSuccess"),MB_OK);
            }
            catch (Exception ex)
            {
               // statusLabel.Text = "Error creating shortcut.";
                NativeMethods.Message(LanguageService.T("ErrorCreating")+$" {ex.Message}", MB_ICONERROR);
            }
        }

        private void CreateShortcut(string desktopPath, string exePath, string rdpPath, Dictionary<string, bool> optionsSelected)
        {
            string name = textBoxName.Text;
            string shortcutPath = Path.Combine(desktopPath, name + ".lnk");

            dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
            dynamic shortcut = shell.CreateShortcut(shortcutPath);

            shortcut.TargetPath = exePath;

            string args = "";

            if (optionsSelected["-all"])
                args = "-all";
            else
            {
                foreach (var opt in optionsSelected)
                    if (opt.Value)
                        args += " " + opt.Key;
            }

            shortcut.Arguments = "\"" + rdpPath + "\" " + args;
            shortcut.WorkingDirectory = Path.GetDirectoryName(exePath);
            shortcut.Description = "RDP Connection: " + name;

            shortcut.IconLocation = @"C:\Windows\System32\mstsc.exe,0";
            shortcut.Save();

            Marshal.FinalReleaseComObject(shortcut);
            Marshal.FinalReleaseComObject(shell);
        }
    }
}