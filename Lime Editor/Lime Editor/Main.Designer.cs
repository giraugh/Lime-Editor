namespace Lime_Editor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Icons = new System.Windows.Forms.ListView();
            this.MenuTopBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Canvas = new System.Windows.Forms.Panel();
            this.comLayerSelect = new System.Windows.Forms.ComboBox();
            this.MenuTopBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 0;
            // 
            // Icons
            // 
            this.Icons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Icons.BackColor = System.Drawing.SystemColors.GrayText;
            this.Icons.Location = new System.Drawing.Point(12, 27);
            this.Icons.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.Icons.Name = "Icons";
            this.Icons.Size = new System.Drawing.Size(229, 698);
            this.Icons.TabIndex = 1;
            this.Icons.UseCompatibleStateImageBehavior = false;
            this.Icons.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateOnMouseMove);
            // 
            // MenuTopBar
            // 
            this.MenuTopBar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.MenuTopBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MenuTopBar.Location = new System.Drawing.Point(0, 0);
            this.MenuTopBar.Name = "MenuTopBar";
            this.MenuTopBar.Size = new System.Drawing.Size(858, 24);
            this.MenuTopBar.TabIndex = 2;
            this.MenuTopBar.Text = "menuStrip1";
            this.MenuTopBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateOnMouseMove);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadProjectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadProjectToolStripMenuItem
            // 
            this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.loadProjectToolStripMenuItem.Text = "Load Project";
            this.loadProjectToolStripMenuItem.Click += new System.EventHandler(this.loadProjectToolStripMenuItem_Click);
            // 
            // Canvas
            // 
            this.Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Canvas.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Canvas.Location = new System.Drawing.Point(248, 57);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(598, 668);
            this.Canvas.TabIndex = 3;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UpdateOnMouseMove);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateOnMouseMove);
            // 
            // comLayerSelect
            // 
            this.comLayerSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comLayerSelect.FormattingEnabled = true;
            this.comLayerSelect.Location = new System.Drawing.Point(248, 30);
            this.comLayerSelect.Name = "comLayerSelect";
            this.comLayerSelect.Size = new System.Drawing.Size(121, 21);
            this.comLayerSelect.TabIndex = 4;
            this.comLayerSelect.SelectedIndexChanged += new System.EventHandler(this.comLayerSelect_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(858, 737);
            this.Controls.Add(this.comLayerSelect);
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.Icons);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MenuTopBar);
            this.MainMenuStrip = this.MenuTopBar;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "Lime Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateOnMouseMove);
            this.MenuTopBar.ResumeLayout(false);
            this.MenuTopBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView Icons;
        private System.Windows.Forms.MenuStrip MenuTopBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
        private System.Windows.Forms.ComboBox comLayerSelect;
    }
}

