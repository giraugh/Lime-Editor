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
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Canvas = new System.Windows.Forms.Panel();
            this.debug = new System.Windows.Forms.Label();
            this.MenuTopBar.SuspendLayout();
            this.Canvas.SuspendLayout();
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
            this.Icons.Size = new System.Drawing.Size(229, 581);
            this.Icons.TabIndex = 1;
            this.Icons.UseCompatibleStateImageBehavior = false;
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
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // Canvas
            // 
            this.Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Canvas.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Canvas.Controls.Add(this.debug);
            this.Canvas.Location = new System.Drawing.Point(248, 28);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(598, 580);
            this.Canvas.TabIndex = 3;
            // 
            // debug
            // 
            this.debug.AutoSize = true;
            this.debug.Location = new System.Drawing.Point(3, 0);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(99, 13);
            this.debug.TabIndex = 0;
            this.debug.Text = "DEBUG CONSOLE";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(858, 620);
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.Icons);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MenuTopBar);
            this.MainMenuStrip = this.MenuTopBar;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "Lime Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MenuTopBar.ResumeLayout(false);
            this.MenuTopBar.PerformLayout();
            this.Canvas.ResumeLayout(false);
            this.Canvas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView Icons;
        private System.Windows.Forms.MenuStrip MenuTopBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.Label debug;
    }
}

