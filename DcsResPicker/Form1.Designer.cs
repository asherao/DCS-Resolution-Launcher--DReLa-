namespace DcsResPicker
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1_dcsPath = new System.Windows.Forms.Label();
            this.label2_optionsPath = new System.Windows.Forms.Label();
            this.button1_selectDCS = new System.Windows.Forms.Button();
            this.button2_selectOptions = new System.Windows.Forms.Button();
            this.textBox1_dcsPath = new System.Windows.Forms.TextBox();
            this.textBox2_optionsPath = new System.Windows.Forms.TextBox();
            this.textBox4_customHeight = new System.Windows.Forms.TextBox();
            this.textBox3_customWidth = new System.Windows.Forms.TextBox();
            this.groupBox1_presetResolutions = new System.Windows.Forms.GroupBox();
            this.button11_2560x1080 = new System.Windows.Forms.Button();
            this.button10_1920x1080 = new System.Windows.Forms.Button();
            this.label4_x = new System.Windows.Forms.Label();
            this.button3_launchDCS = new System.Windows.Forms.Button();
            this.groupBox2_customResolution = new System.Windows.Forms.GroupBox();
            this.label4_customHeight = new System.Windows.Forms.Label();
            this.label3_customWidth = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button5_help = new System.Windows.Forms.Button();
            this.button12_3840x2160 = new System.Windows.Forms.Button();
            this.button13_1280x720 = new System.Windows.Forms.Button();
            this.groupBox1_presetResolutions.SuspendLayout();
            this.groupBox2_customResolution.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1_dcsPath
            // 
            this.label1_dcsPath.AutoSize = true;
            this.label1_dcsPath.Location = new System.Drawing.Point(6, 9);
            this.label1_dcsPath.Name = "label1_dcsPath";
            this.label1_dcsPath.Size = new System.Drawing.Size(77, 13);
            this.label1_dcsPath.TabIndex = 1;
            this.label1_dcsPath.Text = "DCS.exe Path:";
            this.label1_dcsPath.Click += new System.EventHandler(this.label1_dcsPath_Click);
            // 
            // label2_optionsPath
            // 
            this.label2_optionsPath.AutoSize = true;
            this.label2_optionsPath.Location = new System.Drawing.Point(6, 40);
            this.label2_optionsPath.Name = "label2_optionsPath";
            this.label2_optionsPath.Size = new System.Drawing.Size(88, 13);
            this.label2_optionsPath.TabIndex = 2;
            this.label2_optionsPath.Text = "Options.lua Path:";
            // 
            // button1_selectDCS
            // 
            this.button1_selectDCS.Location = new System.Drawing.Point(292, 4);
            this.button1_selectDCS.Name = "button1_selectDCS";
            this.button1_selectDCS.Size = new System.Drawing.Size(103, 23);
            this.button1_selectDCS.TabIndex = 3;
            this.button1_selectDCS.Text = "Select DCS.exe";
            this.button1_selectDCS.UseVisualStyleBackColor = true;
            this.button1_selectDCS.Click += new System.EventHandler(this.button1_selectDCS_Click);
            // 
            // button2_selectOptions
            // 
            this.button2_selectOptions.Location = new System.Drawing.Point(292, 35);
            this.button2_selectOptions.Name = "button2_selectOptions";
            this.button2_selectOptions.Size = new System.Drawing.Size(103, 23);
            this.button2_selectOptions.TabIndex = 4;
            this.button2_selectOptions.Text = "Select options.lua";
            this.button2_selectOptions.UseVisualStyleBackColor = true;
            this.button2_selectOptions.Click += new System.EventHandler(this.button2_selectOptions_Click);
            // 
            // textBox1_dcsPath
            // 
            this.textBox1_dcsPath.Location = new System.Drawing.Point(94, 6);
            this.textBox1_dcsPath.Name = "textBox1_dcsPath";
            this.textBox1_dcsPath.ReadOnly = true;
            this.textBox1_dcsPath.Size = new System.Drawing.Size(192, 20);
            this.textBox1_dcsPath.TabIndex = 5;
            // 
            // textBox2_optionsPath
            // 
            this.textBox2_optionsPath.Location = new System.Drawing.Point(94, 37);
            this.textBox2_optionsPath.Name = "textBox2_optionsPath";
            this.textBox2_optionsPath.ReadOnly = true;
            this.textBox2_optionsPath.Size = new System.Drawing.Size(192, 20);
            this.textBox2_optionsPath.TabIndex = 6;
            // 
            // textBox4_customHeight
            // 
            this.textBox4_customHeight.Location = new System.Drawing.Point(97, 38);
            this.textBox4_customHeight.MaxLength = 5;
            this.textBox4_customHeight.Name = "textBox4_customHeight";
            this.textBox4_customHeight.Size = new System.Drawing.Size(53, 20);
            this.textBox4_customHeight.TabIndex = 9;
            this.textBox4_customHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox4_customHeight_KeyPress);
            // 
            // textBox3_customWidth
            // 
            this.textBox3_customWidth.Location = new System.Drawing.Point(6, 38);
            this.textBox3_customWidth.MaxLength = 5;
            this.textBox3_customWidth.Name = "textBox3_customWidth";
            this.textBox3_customWidth.Size = new System.Drawing.Size(53, 20);
            this.textBox3_customWidth.TabIndex = 8;
            this.textBox3_customWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_customWidth_KeyPress);
            // 
            // groupBox1_presetResolutions
            // 
            this.groupBox1_presetResolutions.Controls.Add(this.button13_1280x720);
            this.groupBox1_presetResolutions.Controls.Add(this.button12_3840x2160);
            this.groupBox1_presetResolutions.Controls.Add(this.button11_2560x1080);
            this.groupBox1_presetResolutions.Controls.Add(this.button10_1920x1080);
            this.groupBox1_presetResolutions.Location = new System.Drawing.Point(9, 64);
            this.groupBox1_presetResolutions.Name = "groupBox1_presetResolutions";
            this.groupBox1_presetResolutions.Size = new System.Drawing.Size(217, 92);
            this.groupBox1_presetResolutions.TabIndex = 10;
            this.groupBox1_presetResolutions.TabStop = false;
            this.groupBox1_presetResolutions.Text = "Preset Resolutions (Launches DCS)";
            // 
            // button11_2560x1080
            // 
            this.button11_2560x1080.Location = new System.Drawing.Point(6, 55);
            this.button11_2560x1080.Name = "button11_2560x1080";
            this.button11_2560x1080.Size = new System.Drawing.Size(100, 23);
            this.button11_2560x1080.TabIndex = 6;
            this.button11_2560x1080.Text = "2560 × 1080";
            this.button11_2560x1080.UseVisualStyleBackColor = true;
            this.button11_2560x1080.Click += new System.EventHandler(this.button11_2560x1080_Click);
            // 
            // button10_1920x1080
            // 
            this.button10_1920x1080.Location = new System.Drawing.Point(112, 22);
            this.button10_1920x1080.Name = "button10_1920x1080";
            this.button10_1920x1080.Size = new System.Drawing.Size(100, 23);
            this.button10_1920x1080.TabIndex = 5;
            this.button10_1920x1080.Text = "1920 × 1080";
            this.button10_1920x1080.UseVisualStyleBackColor = true;
            this.button10_1920x1080.Click += new System.EventHandler(this.button10_1920x1080_Click);
            // 
            // label4_x
            // 
            this.label4_x.AutoSize = true;
            this.label4_x.Location = new System.Drawing.Point(71, 42);
            this.label4_x.Name = "label4_x";
            this.label4_x.Size = new System.Drawing.Size(13, 13);
            this.label4_x.TabIndex = 11;
            this.label4_x.Text = "×";
            // 
            // button3_launchDCS
            // 
            this.button3_launchDCS.Location = new System.Drawing.Point(232, 133);
            this.button3_launchDCS.Name = "button3_launchDCS";
            this.button3_launchDCS.Size = new System.Drawing.Size(160, 23);
            this.button3_launchDCS.TabIndex = 7;
            this.button3_launchDCS.Text = "Launch with Custom Res";
            this.button3_launchDCS.UseVisualStyleBackColor = true;
            this.button3_launchDCS.Click += new System.EventHandler(this.button3_launchDCS_Click);
            // 
            // groupBox2_customResolution
            // 
            this.groupBox2_customResolution.Controls.Add(this.label4_customHeight);
            this.groupBox2_customResolution.Controls.Add(this.textBox3_customWidth);
            this.groupBox2_customResolution.Controls.Add(this.label3_customWidth);
            this.groupBox2_customResolution.Controls.Add(this.textBox4_customHeight);
            this.groupBox2_customResolution.Controls.Add(this.label4_x);
            this.groupBox2_customResolution.Location = new System.Drawing.Point(232, 64);
            this.groupBox2_customResolution.Name = "groupBox2_customResolution";
            this.groupBox2_customResolution.Size = new System.Drawing.Size(160, 63);
            this.groupBox2_customResolution.TabIndex = 13;
            this.groupBox2_customResolution.TabStop = false;
            this.groupBox2_customResolution.Text = "Custom Resolution";
            // 
            // label4_customHeight
            // 
            this.label4_customHeight.AutoSize = true;
            this.label4_customHeight.Location = new System.Drawing.Point(104, 19);
            this.label4_customHeight.Name = "label4_customHeight";
            this.label4_customHeight.Size = new System.Drawing.Size(38, 13);
            this.label4_customHeight.TabIndex = 15;
            this.label4_customHeight.Text = "Height";
            // 
            // label3_customWidth
            // 
            this.label3_customWidth.AutoSize = true;
            this.label3_customWidth.Location = new System.Drawing.Point(15, 19);
            this.label3_customWidth.Name = "label3_customWidth";
            this.label3_customWidth.Size = new System.Drawing.Size(35, 13);
            this.label3_customWidth.TabIndex = 14;
            this.label3_customWidth.Text = "Width";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(9, 162);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(264, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Close DCS Resolution Launcher after starting DCS";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button5_help
            // 
            this.button5_help.Location = new System.Drawing.Point(289, 158);
            this.button5_help.Name = "button5_help";
            this.button5_help.Size = new System.Drawing.Size(103, 23);
            this.button5_help.TabIndex = 15;
            this.button5_help.Text = "Help / Readmee";
            this.button5_help.UseVisualStyleBackColor = true;
            this.button5_help.Click += new System.EventHandler(this.button5_help_Click);
            // 
            // button12_3840x2160
            // 
            this.button12_3840x2160.Location = new System.Drawing.Point(112, 55);
            this.button12_3840x2160.Name = "button12_3840x2160";
            this.button12_3840x2160.Size = new System.Drawing.Size(100, 23);
            this.button12_3840x2160.TabIndex = 7;
            this.button12_3840x2160.Text = "3840 × 2160";
            this.button12_3840x2160.UseVisualStyleBackColor = true;
            this.button12_3840x2160.Click += new System.EventHandler(this.button12_3840x2160_Click);
            // 
            // button13_1280x720
            // 
            this.button13_1280x720.Location = new System.Drawing.Point(6, 22);
            this.button13_1280x720.Name = "button13_1280x720";
            this.button13_1280x720.Size = new System.Drawing.Size(100, 23);
            this.button13_1280x720.TabIndex = 8;
            this.button13_1280x720.Text = "1280 × 720";
            this.button13_1280x720.UseVisualStyleBackColor = true;
            this.button13_1280x720.Click += new System.EventHandler(this.button13_1280x720_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 184);
            this.Controls.Add(this.button5_help);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox2_customResolution);
            this.Controls.Add(this.button3_launchDCS);
            this.Controls.Add(this.groupBox1_presetResolutions);
            this.Controls.Add(this.textBox2_optionsPath);
            this.Controls.Add(this.textBox1_dcsPath);
            this.Controls.Add(this.button2_selectOptions);
            this.Controls.Add(this.button1_selectDCS);
            this.Controls.Add(this.label2_optionsPath);
            this.Controls.Add(this.label1_dcsPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "DCS Resolution Launcher (DReLa) by Bailey";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1_presetResolutions.ResumeLayout(false);
            this.groupBox2_customResolution.ResumeLayout(false);
            this.groupBox2_customResolution.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1_dcsPath;
        private System.Windows.Forms.Label label2_optionsPath;
        private System.Windows.Forms.Button button1_selectDCS;
        private System.Windows.Forms.Button button2_selectOptions;
        private System.Windows.Forms.TextBox textBox1_dcsPath;
        private System.Windows.Forms.TextBox textBox2_optionsPath;
        private System.Windows.Forms.TextBox textBox4_customHeight;
        private System.Windows.Forms.TextBox textBox3_customWidth;
        private System.Windows.Forms.GroupBox groupBox1_presetResolutions;
        private System.Windows.Forms.Button button11_2560x1080;
        private System.Windows.Forms.Button button10_1920x1080;
        private System.Windows.Forms.Label label4_x;
        private System.Windows.Forms.Button button3_launchDCS;
        private System.Windows.Forms.GroupBox groupBox2_customResolution;
        private System.Windows.Forms.Label label4_customHeight;
        private System.Windows.Forms.Label label3_customWidth;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button5_help;
        private System.Windows.Forms.Button button12_3840x2160;
        private System.Windows.Forms.Button button13_1280x720;
    }
}

