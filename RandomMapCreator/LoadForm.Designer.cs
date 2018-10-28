namespace MapGenerator
{
    partial class LoadForm
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
            this.loadLabel = new System.Windows.Forms.Label();
            this.loadListBox = new System.Windows.Forms.ListBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loadLabel
            // 
            this.loadLabel.AutoSize = true;
            this.loadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadLabel.Location = new System.Drawing.Point(36, 36);
            this.loadLabel.Name = "loadLabel";
            this.loadLabel.Size = new System.Drawing.Size(133, 24);
            this.loadLabel.TabIndex = 3;
            this.loadLabel.Text = "Game to Load:";
            // 
            // loadListBox
            // 
            this.loadListBox.FormattingEnabled = true;
            this.loadListBox.ItemHeight = 16;
            this.loadListBox.Location = new System.Drawing.Point(209, 36);
            this.loadListBox.Name = "loadListBox";
            this.loadListBox.Size = new System.Drawing.Size(292, 180);
            this.loadListBox.TabIndex = 4;
            // 
            // loadButton
            // 
            this.loadButton.AutoSize = true;
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(534, 182);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 34);
            this.loadButton.TabIndex = 5;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButtonClicked);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(534, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DeleteButtonClicekd);
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 253);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.loadListBox);
            this.Controls.Add(this.loadLabel);
            this.Name = "LoadForm";
            this.Text = "LoadForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loadLabel;
        private System.Windows.Forms.ListBox loadListBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button button1;
    }
}