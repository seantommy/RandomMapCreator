namespace MapGenerator
{
    partial class DisplayForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.doneButton = new System.Windows.Forms.Button();
            this.heightInput = new System.Windows.Forms.TextBox();
            this.widthInput = new System.Windows.Forms.TextBox();
            this.workingLabel = new System.Windows.Forms.Label();
            this.legendLabel = new System.Windows.Forms.Label();
            this.blackLabel = new System.Windows.Forms.Label();
            this.blackTextLabel = new System.Windows.Forms.Label();
            this.blueLabel = new System.Windows.Forms.Label();
            this.blueTextLabel = new System.Windows.Forms.Label();
            this.redLabel = new System.Windows.Forms.Label();
            this.redTextLabel = new System.Windows.Forms.Label();
            this.floorsDropdown = new System.Windows.Forms.ComboBox();
            this.floorsLabel = new System.Windows.Forms.Label();
            this.displayGrid = new System.Windows.Forms.DataGridView();
            this.fogOfWarLabel = new System.Windows.Forms.Label();
            this.fogOfWarDropdown = new System.Windows.Forms.ComboBox();
            this.greenLabel = new System.Windows.Forms.Label();
            this.greenTextLabel = new System.Windows.Forms.Label();
            this.purpleLabel = new System.Windows.Forms.Label();
            this.purpleTextLabel = new System.Windows.Forms.Label();
            this.floorMessage = new System.Windows.Forms.Label();
            this.floorMaxLabel = new System.Windows.Forms.Label();
            this.roomSizeLabel = new System.Windows.Forms.Label();
            this.roomSizeDropdown = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.displayGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(173, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(651, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please enter your desired map height and width in squares. (min 10, max 60)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(233, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(508, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "For random map size or floor count, leave those fields blank";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightLabel.Location = new System.Drawing.Point(233, 202);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(70, 24);
            this.heightLabel.TabIndex = 4;
            this.heightLabel.Text = "Height:";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthLabel.Location = new System.Drawing.Point(469, 199);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(63, 24);
            this.widthLabel.TabIndex = 5;
            this.widthLabel.Text = "Width:";
            // 
            // doneButton
            // 
            this.doneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doneButton.Location = new System.Drawing.Point(408, 417);
            this.doneButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 31);
            this.doneButton.TabIndex = 6;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // heightInput
            // 
            this.heightInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightInput.Location = new System.Drawing.Point(309, 199);
            this.heightInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.heightInput.Name = "heightInput";
            this.heightInput.Size = new System.Drawing.Size(100, 28);
            this.heightInput.TabIndex = 7;
            // 
            // widthInput
            // 
            this.widthInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthInput.Location = new System.Drawing.Point(545, 199);
            this.widthInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.widthInput.Name = "widthInput";
            this.widthInput.Size = new System.Drawing.Size(100, 28);
            this.widthInput.TabIndex = 8;
            // 
            // workingLabel
            // 
            this.workingLabel.AutoSize = true;
            this.workingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workingLabel.Location = new System.Drawing.Point(343, 156);
            this.workingLabel.Name = "workingLabel";
            this.workingLabel.Size = new System.Drawing.Size(183, 24);
            this.workingLabel.TabIndex = 9;
            this.workingLabel.Text = "Working, please wait";
            this.workingLabel.Visible = false;
            // 
            // legendLabel
            // 
            this.legendLabel.AutoSize = true;
            this.legendLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.legendLabel.Location = new System.Drawing.Point(1144, 207);
            this.legendLabel.Name = "legendLabel";
            this.legendLabel.Size = new System.Drawing.Size(241, 24);
            this.legendLabel.TabIndex = 10;
            this.legendLabel.Text = "Click on a door to move to it";
            this.legendLabel.Visible = false;
            // 
            // blackLabel
            // 
            this.blackLabel.BackColor = System.Drawing.Color.Black;
            this.blackLabel.Location = new System.Drawing.Point(1152, 263);
            this.blackLabel.Name = "blackLabel";
            this.blackLabel.Size = new System.Drawing.Size(15, 15);
            this.blackLabel.TabIndex = 11;
            this.blackLabel.Visible = false;
            // 
            // blackTextLabel
            // 
            this.blackTextLabel.AutoSize = true;
            this.blackTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackTextLabel.Location = new System.Drawing.Point(1199, 261);
            this.blackTextLabel.Name = "blackTextLabel";
            this.blackTextLabel.Size = new System.Drawing.Size(180, 24);
            this.blackTextLabel.TabIndex = 12;
            this.blackTextLabel.Text = "Black indicates walls";
            this.blackTextLabel.Visible = false;
            // 
            // blueLabel
            // 
            this.blueLabel.BackColor = System.Drawing.Color.Blue;
            this.blueLabel.Location = new System.Drawing.Point(1152, 310);
            this.blueLabel.Name = "blueLabel";
            this.blueLabel.Size = new System.Drawing.Size(15, 15);
            this.blueLabel.TabIndex = 13;
            this.blueLabel.Visible = false;
            // 
            // blueTextLabel
            // 
            this.blueTextLabel.AutoSize = true;
            this.blueTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blueTextLabel.Location = new System.Drawing.Point(1199, 308);
            this.blueTextLabel.Name = "blueTextLabel";
            this.blueTextLabel.Size = new System.Drawing.Size(180, 24);
            this.blueTextLabel.TabIndex = 14;
            this.blueTextLabel.Text = "Blue indicates doors";
            this.blueTextLabel.Visible = false;
            // 
            // redLabel
            // 
            this.redLabel.BackColor = System.Drawing.Color.Red;
            this.redLabel.Location = new System.Drawing.Point(1152, 432);
            this.redLabel.Name = "redLabel";
            this.redLabel.Size = new System.Drawing.Size(15, 15);
            this.redLabel.TabIndex = 15;
            this.redLabel.Visible = false;
            // 
            // redTextLabel
            // 
            this.redTextLabel.AutoSize = true;
            this.redTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redTextLabel.Location = new System.Drawing.Point(1199, 427);
            this.redTextLabel.Name = "redTextLabel";
            this.redTextLabel.Size = new System.Drawing.Size(236, 24);
            this.redTextLabel.TabIndex = 16;
            this.redTextLabel.Text = "Red indicates your position";
            this.redTextLabel.Visible = false;
            // 
            // floorsDropdown
            // 
            this.floorsDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floorsDropdown.FormattingEnabled = true;
            this.floorsDropdown.Items.AddRange(new object[] {
            "random",
            "one",
            "two",
            "three"});
            this.floorsDropdown.Location = new System.Drawing.Point(452, 303);
            this.floorsDropdown.Margin = new System.Windows.Forms.Padding(4);
            this.floorsDropdown.Name = "floorsDropdown";
            this.floorsDropdown.Size = new System.Drawing.Size(160, 32);
            this.floorsDropdown.TabIndex = 20;
            this.floorsDropdown.Text = "random";
            this.floorsDropdown.SelectedIndexChanged += new System.EventHandler(this.NumberOfFloorsDropdown_Set);
            // 
            // floorsLabel
            // 
            this.floorsLabel.AutoSize = true;
            this.floorsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floorsLabel.Location = new System.Drawing.Point(263, 303);
            this.floorsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.floorsLabel.Name = "floorsLabel";
            this.floorsLabel.Size = new System.Drawing.Size(154, 24);
            this.floorsLabel.TabIndex = 21;
            this.floorsLabel.Text = "Number of floors:";
            // 
            // displayGrid
            // 
            this.displayGrid.AllowUserToAddRows = false;
            this.displayGrid.AllowUserToDeleteRows = false;
            this.displayGrid.AllowUserToResizeColumns = false;
            this.displayGrid.AllowUserToResizeRows = false;
            this.displayGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.displayGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.displayGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.displayGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.displayGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.displayGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.displayGrid.ColumnHeadersHeight = 11;
            this.displayGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.displayGrid.ColumnHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.displayGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.displayGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.displayGrid.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.displayGrid.Location = new System.Drawing.Point(12, 11);
            this.displayGrid.Margin = new System.Windows.Forms.Padding(0);
            this.displayGrid.MultiSelect = false;
            this.displayGrid.Name = "displayGrid";
            this.displayGrid.ReadOnly = true;
            this.displayGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.displayGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.displayGrid.RowHeadersVisible = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.displayGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.displayGrid.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.displayGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.displayGrid.RowTemplate.Height = 11;
            this.displayGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.displayGrid.ShowCellErrors = false;
            this.displayGrid.ShowCellToolTips = false;
            this.displayGrid.ShowEditingIcon = false;
            this.displayGrid.ShowRowErrors = false;
            this.displayGrid.Size = new System.Drawing.Size(933, 862);
            this.displayGrid.TabIndex = 26;
            this.displayGrid.Visible = false;
            this.displayGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellClicked);
            // 
            // fogOfWarLabel
            // 
            this.fogOfWarLabel.AutoSize = true;
            this.fogOfWarLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fogOfWarLabel.Location = new System.Drawing.Point(249, 354);
            this.fogOfWarLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fogOfWarLabel.Name = "fogOfWarLabel";
            this.fogOfWarLabel.Size = new System.Drawing.Size(167, 24);
            this.fogOfWarLabel.TabIndex = 27;
            this.fogOfWarLabel.Text = "Fog of War setting:";
            // 
            // fogOfWarDropdown
            // 
            this.fogOfWarDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fogOfWarDropdown.FormattingEnabled = true;
            this.fogOfWarDropdown.Items.AddRange(new object[] {
            "No Fog of War",
            "All cleared rooms visible",
            "Only recent rooms visible"});
            this.fogOfWarDropdown.Location = new System.Drawing.Point(452, 350);
            this.fogOfWarDropdown.Margin = new System.Windows.Forms.Padding(4);
            this.fogOfWarDropdown.Name = "fogOfWarDropdown";
            this.fogOfWarDropdown.Size = new System.Drawing.Size(239, 32);
            this.fogOfWarDropdown.TabIndex = 28;
            this.fogOfWarDropdown.Text = "No Fog of War";
            this.fogOfWarDropdown.SelectedIndexChanged += new System.EventHandler(this.FogOfWarDropdownSelected);
            // 
            // greenLabel
            // 
            this.greenLabel.BackColor = System.Drawing.Color.Green;
            this.greenLabel.Location = new System.Drawing.Point(1152, 354);
            this.greenLabel.Name = "greenLabel";
            this.greenLabel.Size = new System.Drawing.Size(15, 15);
            this.greenLabel.TabIndex = 29;
            this.greenLabel.Visible = false;
            // 
            // greenTextLabel
            // 
            this.greenTextLabel.AutoSize = true;
            this.greenTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.greenTextLabel.Location = new System.Drawing.Point(1199, 350);
            this.greenTextLabel.Name = "greenTextLabel";
            this.greenTextLabel.Size = new System.Drawing.Size(283, 24);
            this.greenTextLabel.TabIndex = 30;
            this.greenTextLabel.Text = "Green indicates stairs leading up";
            this.greenTextLabel.Visible = false;
            // 
            // purpleLabel
            // 
            this.purpleLabel.BackColor = System.Drawing.Color.Purple;
            this.purpleLabel.Location = new System.Drawing.Point(1152, 396);
            this.purpleLabel.Name = "purpleLabel";
            this.purpleLabel.Size = new System.Drawing.Size(15, 15);
            this.purpleLabel.TabIndex = 31;
            this.purpleLabel.Visible = false;
            // 
            // purpleTextLabel
            // 
            this.purpleTextLabel.AutoSize = true;
            this.purpleTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purpleTextLabel.Location = new System.Drawing.Point(1199, 391);
            this.purpleTextLabel.Name = "purpleTextLabel";
            this.purpleTextLabel.Size = new System.Drawing.Size(310, 24);
            this.purpleTextLabel.TabIndex = 32;
            this.purpleTextLabel.Text = "Purple indicates stairs leading down";
            this.purpleTextLabel.Visible = false;
            // 
            // floorMessage
            // 
            this.floorMessage.AutoSize = true;
            this.floorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floorMessage.Location = new System.Drawing.Point(1015, 73);
            this.floorMessage.Margin = new System.Windows.Forms.Padding(0);
            this.floorMessage.Name = "floorMessage";
            this.floorMessage.Size = new System.Drawing.Size(123, 39);
            this.floorMessage.TabIndex = 33;
            this.floorMessage.Text = "Floor 1";
            this.floorMessage.Visible = false;
            // 
            // floorMaxLabel
            // 
            this.floorMaxLabel.AutoSize = true;
            this.floorMaxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floorMaxLabel.Location = new System.Drawing.Point(1141, 78);
            this.floorMaxLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.floorMaxLabel.Name = "floorMaxLabel";
            this.floorMaxLabel.Size = new System.Drawing.Size(48, 36);
            this.floorMaxLabel.TabIndex = 34;
            this.floorMaxLabel.Text = "of ";
            this.floorMaxLabel.Visible = false;
            // 
            // roomSizeLabel
            // 
            this.roomSizeLabel.AutoSize = true;
            this.roomSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roomSizeLabel.Location = new System.Drawing.Point(208, 256);
            this.roomSizeLabel.Name = "roomSizeLabel";
            this.roomSizeLabel.Size = new System.Drawing.Size(209, 24);
            this.roomSizeLabel.TabIndex = 35;
            this.roomSizeLabel.Text = "Approximate room size:";
            // 
            // roomSizeDropdown
            // 
            this.roomSizeDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roomSizeDropdown.FormattingEnabled = true;
            this.roomSizeDropdown.Items.AddRange(new object[] {
            "small",
            "average",
            "large"});
            this.roomSizeDropdown.Location = new System.Drawing.Point(452, 256);
            this.roomSizeDropdown.Margin = new System.Windows.Forms.Padding(4);
            this.roomSizeDropdown.Name = "roomSizeDropdown";
            this.roomSizeDropdown.Size = new System.Drawing.Size(160, 32);
            this.roomSizeDropdown.TabIndex = 36;
            this.roomSizeDropdown.Text = "average";
            this.roomSizeDropdown.SelectedIndexChanged += new System.EventHandler(this.RoomSizeDropdown_Set);
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = true;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(1304, 23);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 34);
            this.saveButton.TabIndex = 37;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Visible = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.AutoSize = true;
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(1407, 23);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 34);
            this.loadButton.TabIndex = 38;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // DisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1579, 839);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.roomSizeDropdown);
            this.Controls.Add(this.roomSizeLabel);
            this.Controls.Add(this.floorMaxLabel);
            this.Controls.Add(this.floorMessage);
            this.Controls.Add(this.purpleTextLabel);
            this.Controls.Add(this.purpleLabel);
            this.Controls.Add(this.greenTextLabel);
            this.Controls.Add(this.greenLabel);
            this.Controls.Add(this.fogOfWarDropdown);
            this.Controls.Add(this.fogOfWarLabel);
            this.Controls.Add(this.floorsLabel);
            this.Controls.Add(this.floorsDropdown);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.redTextLabel);
            this.Controls.Add(this.redLabel);
            this.Controls.Add(this.blueTextLabel);
            this.Controls.Add(this.blueLabel);
            this.Controls.Add(this.blackTextLabel);
            this.Controls.Add(this.blackLabel);
            this.Controls.Add(this.legendLabel);
            this.Controls.Add(this.workingLabel);
            this.Controls.Add(this.widthInput);
            this.Controls.Add(this.heightInput);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.displayGrid);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DisplayForm";
            this.Text = "Map Generator";
            ((System.ComponentModel.ISupportInitialize)(this.displayGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.TextBox heightInput;
        private System.Windows.Forms.TextBox widthInput;
        private System.Windows.Forms.Label workingLabel;
        private System.Windows.Forms.Label legendLabel;
        private System.Windows.Forms.Label blackLabel;
        private System.Windows.Forms.Label blackTextLabel;
        private System.Windows.Forms.Label blueLabel;
        private System.Windows.Forms.Label blueTextLabel;
        private System.Windows.Forms.Label redLabel;
        private System.Windows.Forms.Label redTextLabel;
        private System.Windows.Forms.ComboBox floorsDropdown;
        private System.Windows.Forms.Label floorsLabel;
        private System.Windows.Forms.DataGridView displayGrid;
        private System.Windows.Forms.Label fogOfWarLabel;
        private System.Windows.Forms.ComboBox fogOfWarDropdown;
        private System.Windows.Forms.Label greenLabel;
        private System.Windows.Forms.Label greenTextLabel;
        private System.Windows.Forms.Label purpleLabel;
        private System.Windows.Forms.Label purpleTextLabel;
        private System.Windows.Forms.Label floorMessage;
        private System.Windows.Forms.Label floorMaxLabel;
        private System.Windows.Forms.Label roomSizeLabel;
        private System.Windows.Forms.ComboBox roomSizeDropdown;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
    }
}

