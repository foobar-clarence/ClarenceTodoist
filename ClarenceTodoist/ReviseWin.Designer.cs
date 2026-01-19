namespace ClarenceTodoist
{
    partial class ReviseWin
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
            this.nameRevised = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameRevised
            // 
            this.nameRevised.Location = new System.Drawing.Point(256, 170);
            this.nameRevised.Name = "nameRevised";
            this.nameRevised.Size = new System.Drawing.Size(452, 28);
            this.nameRevised.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(107, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "New Name";
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ConfirmBtn.Location = new System.Drawing.Point(330, 268);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(132, 37);
            this.ConfirmBtn.TabIndex = 2;
            this.ConfirmBtn.Text = "Confirm";
            this.ConfirmBtn.UseVisualStyleBackColor = false;
            this.ConfirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.DeleteBtn.Location = new System.Drawing.Point(330, 336);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(132, 37);
            this.DeleteBtn.TabIndex = 3;
            this.DeleteBtn.Text = "Delete";
            this.DeleteBtn.UseVisualStyleBackColor = false;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // ReviseWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 471);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.ConfirmBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameRevised);
            this.Name = "ReviseWin";
            this.Text = "ReviseWin";
            this.Load += new System.EventHandler(this.ReviseWin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameRevised;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.Button DeleteBtn;
    }
}