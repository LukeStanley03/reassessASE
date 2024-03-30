namespace reassessASE
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
            this.runbutton = new System.Windows.Forms.Button();
            this.syntaxbutton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // runbutton
            // 
            this.runbutton.Location = new System.Drawing.Point(535, 459);
            this.runbutton.Name = "runbutton";
            this.runbutton.Size = new System.Drawing.Size(75, 23);
            this.runbutton.TabIndex = 0;
            this.runbutton.Text = "Run";
            this.runbutton.UseVisualStyleBackColor = true;
            this.runbutton.Click += new System.EventHandler(this.runbutton_Click);
            // 
            // syntaxbutton
            // 
            this.syntaxbutton.Location = new System.Drawing.Point(426, 459);
            this.syntaxbutton.Name = "syntaxbutton";
            this.syntaxbutton.Size = new System.Drawing.Size(75, 23);
            this.syntaxbutton.TabIndex = 1;
            this.syntaxbutton.Text = "Syntax";
            this.syntaxbutton.UseVisualStyleBackColor = true;
            this.syntaxbutton.Click += new System.EventHandler(this.syntaxbutton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(158, 459);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(44, 459);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 3;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 546);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.syntaxbutton);
            this.Controls.Add(this.runbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button runbutton;
        private System.Windows.Forms.Button syntaxbutton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button openButton;
    }
}

