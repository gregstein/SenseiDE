
using System.IO;

namespace DE_Sensei
{
    partial class Sensei
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sensei));
            this.styleManager2 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.slidePanel1 = new DevComponents.DotNetBar.Controls.SlidePanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.slidePanel2 = new DevComponents.DotNetBar.Controls.SlidePanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.steamSTATUS = new System.Windows.Forms.PictureBox();
            this.steamLBL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.perfcb = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.label2 = new System.Windows.Forms.Label();
            this.shrinktrack = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.shrinklbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splashswitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label4 = new System.Windows.Forms.Label();
            this.introswitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label5 = new System.Windows.Forms.Label();
            this.effectswitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.efconfig = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.modswitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.runas = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.screenopt = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dpiscal = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.listsubmitee = new DevComponents.DotNetBar.ListBoxAdv();
            this.label8 = new System.Windows.Forms.Label();
            this.modcount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.itemselec = new System.Windows.Forms.Label();
            this.selectmods = new System.Windows.Forms.CheckBox();
            this.offinstall = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.exportit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.deleteit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.browsemod = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.refreshmods = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.pBAR = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.pBAR2 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.TickWorks = new System.Windows.Forms.Timer(this.components);
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.slidePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.slidePanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.steamSTATUS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efconfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // styleManager2
            // 
            this.styleManager2.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager2.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242))))), System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204))))));
            // 
            // slidePanel1
            // 
            this.slidePanel1.Controls.Add(this.pictureBox2);
            this.slidePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.slidePanel1.Location = new System.Drawing.Point(0, 0);
            this.slidePanel1.Name = "slidePanel1";
            this.slidePanel1.Size = new System.Drawing.Size(492, 80);
            this.slidePanel1.TabIndex = 0;
            this.slidePanel1.Text = "slidePanel1";
            this.slidePanel1.UsesBlockingAnimation = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Image = global::DE_Sensei.Properties.Resources.sensei;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(492, 80);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // slidePanel2
            // 
            this.slidePanel2.Controls.Add(this.flowLayoutPanel1);
            this.slidePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slidePanel2.Location = new System.Drawing.Point(0, 80);
            this.slidePanel2.Name = "slidePanel2";
            this.slidePanel2.Size = new System.Drawing.Size(492, 483);
            this.slidePanel2.TabIndex = 1;
            this.slidePanel2.Text = "slidePanel2";
            this.slidePanel2.UsesBlockingAnimation = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.steamSTATUS);
            this.flowLayoutPanel1.Controls.Add(this.steamLBL);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.perfcb);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.shrinktrack);
            this.flowLayoutPanel1.Controls.Add(this.shrinklbl);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.splashswitch);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.introswitch);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.effectswitch);
            this.flowLayoutPanel1.Controls.Add(this.efconfig);
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.modswitch);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox3);
            this.flowLayoutPanel1.Controls.Add(this.runas);
            this.flowLayoutPanel1.Controls.Add(this.screenopt);
            this.flowLayoutPanel1.Controls.Add(this.dpiscal);
            this.flowLayoutPanel1.Controls.Add(this.listsubmitee);
            this.flowLayoutPanel1.Controls.Add(this.label8);
            this.flowLayoutPanel1.Controls.Add(this.modcount);
            this.flowLayoutPanel1.Controls.Add(this.label9);
            this.flowLayoutPanel1.Controls.Add(this.itemselec);
            this.flowLayoutPanel1.Controls.Add(this.selectmods);
            this.flowLayoutPanel1.Controls.Add(this.offinstall);
            this.flowLayoutPanel1.Controls.Add(this.exportit);
            this.flowLayoutPanel1.Controls.Add(this.deleteit);
            this.flowLayoutPanel1.Controls.Add(this.browsemod);
            this.flowLayoutPanel1.Controls.Add(this.refreshmods);
            this.flowLayoutPanel1.Controls.Add(this.pBAR);
            this.flowLayoutPanel1.Controls.Add(this.pBAR2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(959, 477);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // steamSTATUS
            // 
            this.steamSTATUS.Image = global::DE_Sensei.Properties.Resources.steam_red;
            this.steamSTATUS.Location = new System.Drawing.Point(3, 3);
            this.steamSTATUS.Name = "steamSTATUS";
            this.steamSTATUS.Size = new System.Drawing.Size(24, 25);
            this.steamSTATUS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.steamSTATUS.TabIndex = 29;
            this.steamSTATUS.TabStop = false;
            // 
            // steamLBL
            // 
            this.steamLBL.AutoSize = true;
            this.steamLBL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.SetFlowBreak(this.steamLBL, true);
            this.steamLBL.Font = new System.Drawing.Font("Tahoma", 10F);
            this.steamLBL.Location = new System.Drawing.Point(33, 0);
            this.steamLBL.Name = "steamLBL";
            this.steamLBL.Size = new System.Drawing.Size(75, 31);
            this.steamLBL.TabIndex = 28;
            this.steamLBL.Text = "Steam OFF";
            this.steamLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Graphics Profiles     ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // perfcb
            // 
            this.perfcb.DisplayMember = "Text";
            this.perfcb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.perfcb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.perfcb.Enabled = false;
            this.flowLayoutPanel1.SetFlowBreak(this.perfcb, true);
            this.perfcb.FormattingEnabled = true;
            this.perfcb.ItemHeight = 15;
            this.perfcb.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.perfcb.Location = new System.Drawing.Point(151, 34);
            this.perfcb.Name = "perfcb";
            this.perfcb.Size = new System.Drawing.Size(136, 21);
            this.perfcb.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.perfcb.TabIndex = 2;
            this.perfcb.SelectedIndexChanged += new System.EventHandler(this.perfcb_SelectedIndexChanged);
            this.perfcb.SelectedValueChanged += new System.EventHandler(this.perfcb_SelectedValueChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "High Performance";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "Default";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "Shrink Resolution    ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shrinktrack
            // 
            this.shrinktrack.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderForm;
            this.shrinktrack.DrawBackground = true;
            this.shrinktrack.Enabled = false;
            this.shrinktrack.Location = new System.Drawing.Point(150, 61);
            this.shrinktrack.Maximum = 100;
            this.shrinktrack.Minimum = 10;
            this.shrinktrack.Name = "shrinktrack";
            this.shrinktrack.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.shrinktrack.Size = new System.Drawing.Size(135, 27);
            this.shrinktrack.TabIndex = 34;
            this.shrinktrack.Value = 100;
            this.shrinktrack.ValueChanged += new System.EventHandler(this.shrinktrack_ValueChanged);
            // 
            // shrinklbl
            // 
            this.shrinklbl.AutoSize = true;
            this.shrinklbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.SetFlowBreak(this.shrinklbl, true);
            this.shrinklbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shrinklbl.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.shrinklbl.Location = new System.Drawing.Point(291, 58);
            this.shrinklbl.Name = "shrinklbl";
            this.shrinklbl.Size = new System.Drawing.Size(45, 33);
            this.shrinklbl.TabIndex = 35;
            this.shrinklbl.Text = "100%";
            this.shrinklbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "Optimize Splash Screen";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splashswitch
            // 
            // 
            // 
            // 
            this.splashswitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.splashswitch.Enabled = false;
            this.flowLayoutPanel1.SetFlowBreak(this.splashswitch, true);
            this.splashswitch.Location = new System.Drawing.Point(176, 94);
            this.splashswitch.Name = "splashswitch";
            this.splashswitch.Size = new System.Drawing.Size(66, 22);
            this.splashswitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.splashswitch.TabIndex = 6;
            this.splashswitch.ValueChanged += new System.EventHandler(this.splashswitch_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 28);
            this.label4.TabIndex = 7;
            this.label4.Text = "Disable Intro Videos       ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // introswitch
            // 
            // 
            // 
            // 
            this.introswitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.introswitch.Enabled = false;
            this.flowLayoutPanel1.SetFlowBreak(this.introswitch, true);
            this.introswitch.Location = new System.Drawing.Point(176, 122);
            this.introswitch.Name = "introswitch";
            this.introswitch.Size = new System.Drawing.Size(66, 22);
            this.introswitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.introswitch.TabIndex = 8;
            this.introswitch.ValueChanged += new System.EventHandler(this.introswitch_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 28);
            this.label5.TabIndex = 9;
            this.label5.Text = "Disable Hidden Effects  ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // effectswitch
            // 
            // 
            // 
            // 
            this.effectswitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.effectswitch.Enabled = false;
            this.effectswitch.Location = new System.Drawing.Point(174, 150);
            this.effectswitch.Name = "effectswitch";
            this.effectswitch.Size = new System.Drawing.Size(66, 22);
            this.effectswitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.effectswitch.TabIndex = 10;
            this.effectswitch.ValueChanged += new System.EventHandler(this.effectswitch_ValueChanged);
            // 
            // efconfig
            // 
            this.efconfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.efconfig.Enabled = false;
            this.flowLayoutPanel1.SetFlowBreak(this.efconfig, true);
            this.efconfig.Image = global::DE_Sensei.Properties.Resources.gear;
            this.efconfig.Location = new System.Drawing.Point(246, 150);
            this.efconfig.Name = "efconfig";
            this.efconfig.Size = new System.Drawing.Size(22, 22);
            this.efconfig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.efconfig.TabIndex = 32;
            this.efconfig.TabStop = false;
            this.efconfig.Click += new System.EventHandler(this.efconfig_Click);
            this.efconfig.MouseLeave += new System.EventHandler(this.efconfig_MouseLeave);
            this.efconfig.MouseHover += new System.EventHandler(this.efconfig_MouseHover);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 28);
            this.label6.TabIndex = 11;
            this.label6.Text = "Offline Mods Manager   ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // modswitch
            // 
            // 
            // 
            // 
            this.modswitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.flowLayoutPanel1.SetFlowBreak(this.modswitch, true);
            this.modswitch.Location = new System.Drawing.Point(175, 178);
            this.modswitch.Name = "modswitch";
            this.modswitch.Size = new System.Drawing.Size(66, 22);
            this.modswitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.modswitch.TabIndex = 13;
            this.modswitch.Value = true;
            this.modswitch.ValueObject = "Y";
            this.modswitch.ValueChanged += new System.EventHandler(this.modswitch_ValueChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DE_Sensei.Properties.Resources.deico;
            this.pictureBox3.Location = new System.Drawing.Point(3, 206);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 25);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 40;
            this.pictureBox3.TabStop = false;
            // 
            // runas
            // 
            this.runas.AutoSize = true;
            // 
            // 
            // 
            this.runas.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.runas.Enabled = false;
            this.runas.Location = new System.Drawing.Point(37, 210);
            this.runas.Margin = new System.Windows.Forms.Padding(7);
            this.runas.Name = "runas";
            this.runas.Size = new System.Drawing.Size(93, 15);
            this.runas.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.runas.TabIndex = 37;
            this.runas.Text = "Run As Admin";
            this.runas.CheckedChanged += new System.EventHandler(this.runas_CheckedChanged);
            // 
            // screenopt
            // 
            this.screenopt.AutoSize = true;
            // 
            // 
            // 
            this.screenopt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.screenopt.Enabled = false;
            this.screenopt.Location = new System.Drawing.Point(144, 210);
            this.screenopt.Margin = new System.Windows.Forms.Padding(7);
            this.screenopt.Name = "screenopt";
            this.screenopt.Size = new System.Drawing.Size(182, 15);
            this.screenopt.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.screenopt.TabIndex = 38;
            this.screenopt.Text = "Disable Full Screen Optimization";
            this.screenopt.CheckedChanged += new System.EventHandler(this.screenopt_CheckedChanged);
            // 
            // dpiscal
            // 
            this.dpiscal.AutoSize = true;
            // 
            // 
            // 
            this.dpiscal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpiscal.Enabled = false;
            this.flowLayoutPanel1.SetFlowBreak(this.dpiscal, true);
            this.dpiscal.Location = new System.Drawing.Point(340, 210);
            this.dpiscal.Margin = new System.Windows.Forms.Padding(7);
            this.dpiscal.Name = "dpiscal";
            this.dpiscal.Size = new System.Drawing.Size(125, 15);
            this.dpiscal.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dpiscal.TabIndex = 39;
            this.dpiscal.Text = "DPI Scaling Override";
            this.dpiscal.CheckedChanged += new System.EventHandler(this.dpiscal_CheckedChanged);
            // 
            // listsubmitee
            // 
            this.listsubmitee.AutoScroll = true;
            // 
            // 
            // 
            this.listsubmitee.BackgroundStyle.Class = "ListBoxAdv";
            this.listsubmitee.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listsubmitee.ContainerControlProcessDialogKey = true;
            this.listsubmitee.DragDropSupport = true;
            this.flowLayoutPanel1.SetFlowBreak(this.listsubmitee, true);
            this.listsubmitee.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listsubmitee.ImageSize = DevComponents.DotNetBar.eBarImageSize.Large;
            this.listsubmitee.Location = new System.Drawing.Point(3, 237);
            this.listsubmitee.Name = "listsubmitee";
            this.listsubmitee.SelectionMode = DevComponents.DotNetBar.eSelectionMode.MultiSimple;
            this.listsubmitee.Size = new System.Drawing.Size(473, 122);
            this.listsubmitee.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.listsubmitee.TabIndex = 7;
            this.listsubmitee.SelectedIndexChanged += new System.EventHandler(this.listsubmitee_SelectedIndexChanged);
            this.listsubmitee.ItemClick += new System.EventHandler(this.listsubmitee_ItemClick);
            this.listsubmitee.ItemAdded += new System.EventHandler(this.listsubmitee_ItemAdded);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 362);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 16);
            this.label8.TabIndex = 23;
            this.label8.Text = "Mods Count:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // modcount
            // 
            this.modcount.AutoSize = true;
            this.modcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modcount.ForeColor = System.Drawing.Color.Maroon;
            this.modcount.Location = new System.Drawing.Point(102, 362);
            this.modcount.Name = "modcount";
            this.modcount.Size = new System.Drawing.Size(16, 16);
            this.modcount.TabIndex = 22;
            this.modcount.Text = "0";
            this.modcount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(124, 362);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(139, 16);
            this.label9.TabIndex = 24;
            this.label9.Text = "Currently Selected:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // itemselec
            // 
            this.itemselec.AutoSize = true;
            this.itemselec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemselec.ForeColor = System.Drawing.Color.SeaGreen;
            this.itemselec.Location = new System.Drawing.Point(269, 362);
            this.itemselec.Name = "itemselec";
            this.itemselec.Size = new System.Drawing.Size(16, 16);
            this.itemselec.TabIndex = 25;
            this.itemselec.Text = "0";
            this.itemselec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectmods
            // 
            this.selectmods.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.selectmods, true);
            this.selectmods.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.selectmods.ForeColor = System.Drawing.SystemColors.Highlight;
            this.selectmods.Location = new System.Drawing.Point(291, 365);
            this.selectmods.Name = "selectmods";
            this.selectmods.Size = new System.Drawing.Size(93, 20);
            this.selectmods.TabIndex = 30;
            this.selectmods.Text = "Select All";
            this.selectmods.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.selectmods.UseVisualStyleBackColor = true;
            this.selectmods.CheckedChanged += new System.EventHandler(this.selectmods_CheckedChanged);
            // 
            // offinstall
            // 
            this.offinstall.AutoSize = true;
            this.offinstall.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Cluster;
            this.offinstall.Enabled = false;
            this.offinstall.Location = new System.Drawing.Point(3, 391);
            this.offinstall.Name = "offinstall";
            this.offinstall.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.offinstall.Size = new System.Drawing.Size(122, 38);
            this.offinstall.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offinstall.TabIndex = 19;
            this.offinstall.Values.Image = global::DE_Sensei.Properties.Resources.offline_4033;
            this.offinstall.Values.Text = "Install Offline";
            this.offinstall.Click += new System.EventHandler(this.offinstall_Click);
            // 
            // exportit
            // 
            this.exportit.AutoSize = true;
            this.exportit.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Cluster;
            this.exportit.Enabled = false;
            this.exportit.Location = new System.Drawing.Point(131, 391);
            this.exportit.Name = "exportit";
            this.exportit.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.exportit.Size = new System.Drawing.Size(81, 38);
            this.exportit.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportit.TabIndex = 20;
            this.exportit.Values.Image = global::DE_Sensei.Properties.Resources.archive;
            this.exportit.Values.Text = "Export";
            this.exportit.Click += new System.EventHandler(this.exportit_Click);
            // 
            // deleteit
            // 
            this.deleteit.AutoSize = true;
            this.deleteit.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Cluster;
            this.deleteit.Enabled = false;
            this.deleteit.Location = new System.Drawing.Point(218, 391);
            this.deleteit.Name = "deleteit";
            this.deleteit.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.deleteit.Size = new System.Drawing.Size(71, 38);
            this.deleteit.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteit.TabIndex = 21;
            this.deleteit.Values.Image = global::DE_Sensei.Properties.Resources.delete;
            this.deleteit.Values.Text = "Delete";
            this.deleteit.Click += new System.EventHandler(this.deleteit_Click);
            // 
            // browsemod
            // 
            this.browsemod.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Cluster;
            this.browsemod.Enabled = false;
            this.browsemod.Location = new System.Drawing.Point(295, 391);
            this.browsemod.Name = "browsemod";
            this.browsemod.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.browsemod.Size = new System.Drawing.Size(89, 38);
            this.browsemod.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browsemod.TabIndex = 31;
            this.browsemod.Values.Image = global::DE_Sensei.Properties.Resources.browse;
            this.browsemod.Values.Text = "Browse";
            this.browsemod.Click += new System.EventHandler(this.browsemod_Click);
            // 
            // refreshmods
            // 
            this.refreshmods.AutoSize = true;
            this.refreshmods.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Cluster;
            this.refreshmods.Enabled = false;
            this.flowLayoutPanel1.SetFlowBreak(this.refreshmods, true);
            this.refreshmods.Location = new System.Drawing.Point(390, 391);
            this.refreshmods.Name = "refreshmods";
            this.refreshmods.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.refreshmods.Size = new System.Drawing.Size(87, 38);
            this.refreshmods.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshmods.TabIndex = 26;
            this.refreshmods.Values.Image = global::DE_Sensei.Properties.Resources.refresh;
            this.refreshmods.Values.Text = "Refresh";
            this.refreshmods.Click += new System.EventHandler(this.refreshmods_Click);
            // 
            // pBAR
            // 
            // 
            // 
            // 
            this.pBAR.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.flowLayoutPanel1.SetFlowBreak(this.pBAR, true);
            this.pBAR.Location = new System.Drawing.Point(3, 435);
            this.pBAR.Name = "pBAR";
            this.pBAR.Size = new System.Drawing.Size(474, 20);
            this.pBAR.TabIndex = 33;
            this.pBAR.Text = "progressBarX1";
            // 
            // pBAR2
            // 
            // 
            // 
            // 
            this.pBAR2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pBAR2.Location = new System.Drawing.Point(3, 461);
            this.pBAR2.Name = "pBAR2";
            this.pBAR2.Size = new System.Drawing.Size(473, 10);
            this.pBAR2.TabIndex = 36;
            this.pBAR2.Text = "progressBarX1";
            // 
            // TickWorks
            // 
            this.TickWorks.Enabled = true;
            this.TickWorks.Tick += new System.EventHandler(this.TickWorks_Tick);
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "comboItem3";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "comboItem2";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "comboItem1";
            // 
            // Sensei
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(492, 563);
            this.Controls.Add(this.slidePanel2);
            this.Controls.Add(this.slidePanel1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sensei";
            this.Text = "Sensei DE - Improve The Performance Of Age of Empires II DE";
            this.Load += new System.EventHandler(this.Sensei_Load);
            this.slidePanel1.ResumeLayout(false);
            this.slidePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.slidePanel2.ResumeLayout(false);
            this.slidePanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.steamSTATUS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efconfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager2;
        private DevComponents.DotNetBar.Controls.SlidePanel slidePanel1;
        private DevComponents.DotNetBar.Controls.SlidePanel slidePanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx perfcb;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.SwitchButton splashswitch;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.SwitchButton introswitch;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.SwitchButton effectswitch;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.SwitchButton modswitch;
        private ComponentFactory.Krypton.Toolkit.KryptonButton offinstall;
        private ComponentFactory.Krypton.Toolkit.KryptonButton exportit;
        private ComponentFactory.Krypton.Toolkit.KryptonButton deleteit;
        private DevComponents.DotNetBar.ListBoxAdv listsubmitee;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label modcount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label itemselec;
        private ComponentFactory.Krypton.Toolkit.KryptonButton refreshmods;
        private System.Windows.Forms.Timer TickWorks;
        private System.Windows.Forms.PictureBox steamSTATUS;
        private System.Windows.Forms.Label steamLBL;
        private System.Windows.Forms.CheckBox selectmods;
        private ComponentFactory.Krypton.Toolkit.KryptonButton browsemod;
        private System.Windows.Forms.PictureBox efconfig;
        private DevComponents.DotNetBar.Controls.ProgressBarX pBAR;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar shrinktrack;
        private System.Windows.Forms.Label shrinklbl;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.DotNetBar.Controls.ProgressBarX pBAR2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private DevComponents.DotNetBar.Controls.CheckBoxX runas;
        private DevComponents.DotNetBar.Controls.CheckBoxX screenopt;
        private DevComponents.DotNetBar.Controls.CheckBoxX dpiscal;
    }
}