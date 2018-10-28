namespace MapGenerator
{
    partial class SaveForm
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
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveListBox = new System.Windows.Forms.ListBox();
            this.existingSavesLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameTextbox
            // 
            this.nameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextbox.Location = new System.Drawing.Point(172, 238);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.Size = new System.Drawing.Size(296, 28);
            this.nameTextbox.TabIndex = 0;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(22, 242);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(113, 24);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Save Name:";
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = true;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(358, 310);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(82, 34);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButtonClicked);
            // 
            // saveListBox
            // 
            this.saveListBox.FormattingEnabled = true;
            this.saveListBox.ItemHeight = 16;
            this.saveListBox.Location = new System.Drawing.Point(176, 25);
            this.saveListBox.Name = "saveListBox";
            this.saveListBox.Size = new System.Drawing.Size(292, 180);
            this.saveListBox.TabIndex = 6;
            this.saveListBox.SelectedIndexChanged += new System.EventHandler(this.SaveListItemSelected);
            // 
            // existingSavesLabel
            // 
            this.existingSavesLabel.AutoSize = true;
            this.existingSavesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.existingSavesLabel.Location = new System.Drawing.Point(3, 25);
            this.existingSavesLabel.Name = "existingSavesLabel";
            this.existingSavesLabel.Size = new System.Drawing.Size(137, 24);
            this.existingSavesLabel.TabIndex = 5;
            this.existingSavesLabel.Text = "Existing Saves:";
            // 
            // deleteButton
            // 
            this.deleteButton.AutoSize = true;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(172, 310);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 34);
            this.deleteButton.TabIndex = 7;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButtonClicekd);
            // 
            // SaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 373);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.saveListBox);
            this.Controls.Add(this.existingSavesLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameTextbox);
            this.Name = "SaveForm";
            this.Text = "SaveForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextbox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ListBox saveListBox;
        private System.Windows.Forms.Label existingSavesLabel;
        private System.Windows.Forms.Button deleteButton;
    }
}