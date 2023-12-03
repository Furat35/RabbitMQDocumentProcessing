namespace EDevlet.Document.Request
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnConnect = new Button();
            btnCreateDocument = new Button();
            txtConnectionString = new TextBox();
            label1 = new Label();
            txtLog = new RichTextBox();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(448, 14);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(135, 38);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnCreateDocument
            // 
            btnCreateDocument.Enabled = false;
            btnCreateDocument.Location = new Point(197, 82);
            btnCreateDocument.Name = "btnCreateDocument";
            btnCreateDocument.Size = new Size(222, 74);
            btnCreateDocument.TabIndex = 0;
            btnCreateDocument.Text = "Create Document";
            btnCreateDocument.UseVisualStyleBackColor = true;
            btnCreateDocument.Click += btnCreateDocument_Click;
            // 
            // txtConnectionString
            // 
            txtConnectionString.Location = new Point(142, 20);
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(277, 27);
            txtConnectionString.TabIndex = 1;
            txtConnectionString.Text = "amqp://guest:guest@localhost:5672";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 23);
            label1.Name = "label1";
            label1.Size = new Size(134, 20);
            label1.TabIndex = 2;
            label1.Text = "Connection String: ";
            // 
            // txtLog
            // 
            txtLog.Location = new Point(11, 179);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(572, 198);
            txtLog.TabIndex = 3;
            txtLog.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(596, 387);
            Controls.Add(txtLog);
            Controls.Add(label1);
            Controls.Add(txtConnectionString);
            Controls.Add(btnCreateDocument);
            Controls.Add(btnConnect);
            Name = "Form1";
            Text = "EDevlet";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private Button btnCreateDocument;
        private TextBox txtConnectionString;
        private Label label1;
        private RichTextBox txtLog;
    }
}