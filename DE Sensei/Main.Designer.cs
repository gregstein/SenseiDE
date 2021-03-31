
namespace DE_Sensei
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.eflayer = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.reloadGIF = new System.Windows.Forms.PictureBox();
            this.selectcbs = new System.Windows.Forms.CheckBox();
            this.efoff = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.progCOPY = new System.Windows.Forms.ProgressBar();
            this.restef = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.eflayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reloadGIF)).BeginInit();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242))))), System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204))))));
            // 
            // eflayer
            // 
            this.eflayer.AutoScroll = true;
            this.eflayer.Controls.Add(this.pictureBox1);
            this.eflayer.Controls.Add(this.reloadGIF);
            this.eflayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.eflayer.Location = new System.Drawing.Point(0, 0);
            this.eflayer.Name = "eflayer";
            this.eflayer.Size = new System.Drawing.Size(393, 342);
            this.eflayer.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DE_Sensei.Properties.Resources.checkeffects;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(387, 46);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // reloadGIF
            // 
            this.reloadGIF.Image = global::DE_Sensei.Properties.Resources.Blocks_preloader;
            this.reloadGIF.Location = new System.Drawing.Point(3, 55);
            this.reloadGIF.Name = "reloadGIF";
            this.reloadGIF.Size = new System.Drawing.Size(387, 282);
            this.reloadGIF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.reloadGIF.TabIndex = 35;
            this.reloadGIF.TabStop = false;
            // 
            // selectcbs
            // 
            this.selectcbs.AutoSize = true;
            this.selectcbs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.selectcbs.ForeColor = System.Drawing.SystemColors.Highlight;
            this.selectcbs.Location = new System.Drawing.Point(12, 348);
            this.selectcbs.Name = "selectcbs";
            this.selectcbs.Size = new System.Drawing.Size(93, 20);
            this.selectcbs.TabIndex = 31;
            this.selectcbs.Text = "Select All";
            this.selectcbs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.selectcbs.UseVisualStyleBackColor = true;
            this.selectcbs.CheckedChanged += new System.EventHandler(this.selectcbs_CheckedChanged);
            // 
            // efoff
            // 
            this.efoff.AutoSize = true;
            this.efoff.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.FormClose;
            this.efoff.Location = new System.Drawing.Point(185, 343);
            this.efoff.Name = "efoff";
            this.efoff.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Black;
            this.efoff.Size = new System.Drawing.Size(182, 38);
            this.efoff.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.efoff.TabIndex = 32;
            this.efoff.Values.Image = global::DE_Sensei.Properties.Resources.disable;
            this.efoff.Values.Text = "Turn Off Selected Effects";
            this.efoff.Click += new System.EventHandler(this.efoff_Click);
            // 
            // progCOPY
            // 
            this.progCOPY.Location = new System.Drawing.Point(3, 374);
            this.progCOPY.Name = "progCOPY";
            this.progCOPY.Size = new System.Drawing.Size(176, 17);
            this.progCOPY.TabIndex = 0;
            // 
            // restef
            // 
            this.restef.AutoSize = true;
            this.restef.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.FormClose;
            this.restef.Location = new System.Drawing.Point(185, 387);
            this.restef.Name = "restef";
            this.restef.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Black;
            this.restef.Size = new System.Drawing.Size(105, 38);
            this.restef.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restef.TabIndex = 33;
            this.restef.Values.Image = global::DE_Sensei.Properties.Resources.turnon;
            this.restef.Values.Text = "Restore All";
            this.restef.Click += new System.EventHandler(this.restef_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 432);
            this.Controls.Add(this.restef);
            this.Controls.Add(this.progCOPY);
            this.Controls.Add(this.efoff);
            this.Controls.Add(this.selectcbs);
            this.Controls.Add(this.eflayer);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Game Effects - DE Sensei";
            this.Load += new System.EventHandler(this.Main_Load);
            this.eflayer.ResumeLayout(false);
            this.eflayer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reloadGIF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.FlowLayoutPanel eflayer;
        private System.Windows.Forms.CheckBox selectcbs;
        private ComponentFactory.Krypton.Toolkit.KryptonButton efoff;
        private System.Windows.Forms.ProgressBar progCOPY;
        private ComponentFactory.Krypton.Toolkit.KryptonButton restef;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox reloadGIF;
    }
}