namespace NetFree_Check_Issues
{
    partial class CheckIssues
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckIssues));
            this.button1 = new System.Windows.Forms.Button();
            this.TimeCorrect = new System.Windows.Forms.Label();
            this.Internet = new System.Windows.Forms.Label();
            this.Cert = new System.Windows.Forms.Label();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.ISP = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.loader = new System.Windows.Forms.PictureBox();
            this.layout = new System.Windows.Forms.Panel();
            this.fixcert = new System.Windows.Forms.Button();
            this.fixtime = new System.Windows.Forms.Button();
            this.table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loader)).BeginInit();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(194, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 47);
            this.button1.TabIndex = 1;
            this.button1.Text = "בדיקה חוזרת";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TimeCorrect
            // 
            this.TimeCorrect.AutoSize = true;
            this.TimeCorrect.Location = new System.Drawing.Point(81, 81);
            this.TimeCorrect.Name = "TimeCorrect";
            this.TimeCorrect.Size = new System.Drawing.Size(83, 13);
            this.TimeCorrect.TabIndex = 2;
            this.TimeCorrect.Text = "תאריך לא נכון";
            // 
            // Internet
            // 
            this.Internet.AutoSize = true;
            this.Internet.Location = new System.Drawing.Point(61, 0);
            this.Internet.Name = "Internet";
            this.Internet.Size = new System.Drawing.Size(103, 13);
            this.Internet.TabIndex = 3;
            this.Internet.Text = "אינטרנט לא מחובר";
            // 
            // Cert
            // 
            this.Cert.AutoSize = true;
            this.Cert.Location = new System.Drawing.Point(72, 54);
            this.Cert.Name = "Cert";
            this.Cert.Size = new System.Drawing.Size(92, 13);
            this.Cert.TabIndex = 4;
            this.Cert.Text = "תעודה לא מותקן";
            // 
            // table
            // 
            this.table.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.Controls.Add(this.Internet, 0, 0);
            this.table.Controls.Add(this.ISP, 0, 1);
            this.table.Controls.Add(this.TimeCorrect, 0, 3);
            this.table.Controls.Add(this.Cert, 0, 2);
            this.table.Cursor = System.Windows.Forms.Cursors.Default;
            this.table.Location = new System.Drawing.Point(31, 95);
            this.table.Name = "table";
            this.table.RowCount = 4;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.Size = new System.Drawing.Size(167, 108);
            this.table.TabIndex = 5;
            // 
            // ISP
            // 
            this.ISP.AutoSize = true;
            this.ISP.Location = new System.Drawing.Point(83, 27);
            this.ISP.Name = "ISP";
            this.ISP.Size = new System.Drawing.Size(81, 13);
            this.ISP.TabIndex = 1;
            this.ISP.Text = "ספק לא מחובר";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(209, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(114, 82);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // loader
            // 
            this.loader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loader.Image = ((System.Drawing.Image)(resources.GetObject("loader.Image")));
            this.loader.ImageLocation = "";
            this.loader.InitialImage = ((System.Drawing.Image)(resources.GetObject("loader.InitialImage")));
            this.loader.Location = new System.Drawing.Point(12, 5);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(33, 33);
            this.loader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loader.TabIndex = 7;
            this.loader.TabStop = false;
            this.loader.Visible = false;
            // 
            // layout
            // 
            this.layout.Controls.Add(this.fixcert);
            this.layout.Controls.Add(this.fixtime);
            this.layout.Location = new System.Drawing.Point(31, 95);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(460, 107);
            this.layout.TabIndex = 9;
            // 
            // fixcert
            // 
            this.fixcert.Location = new System.Drawing.Point(219, 54);
            this.fixcert.Name = "fixcert";
            this.fixcert.Size = new System.Drawing.Size(68, 21);
            this.fixcert.TabIndex = 11;
            this.fixcert.Text = "התקן כעת";
            this.fixcert.UseVisualStyleBackColor = true;
            this.fixcert.Click += new System.EventHandler(this.fixcert_Click);
            // 
            // fixtime
            // 
            this.fixtime.Location = new System.Drawing.Point(219, 81);
            this.fixtime.Name = "fixtime";
            this.fixtime.Size = new System.Drawing.Size(68, 23);
            this.fixtime.TabIndex = 9;
            this.fixtime.Text = "תקן כעת";
            this.fixtime.UseVisualStyleBackColor = true;
            this.fixtime.Click += new System.EventHandler(this.button3_Click);
            // 
            // CheckIssues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 266);
            this.Controls.Add(this.loader);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.table);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.layout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CheckIssues";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "בדיקת בעיות BETA";
            this.Load += new System.EventHandler(this.CheckIssues_Load);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loader)).EndInit();
            this.layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label TimeCorrect;
        private System.Windows.Forms.Label Internet;
        private System.Windows.Forms.Label Cert;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Label ISP;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox loader;
        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.Button fixcert;
        private System.Windows.Forms.Button fixtime;
    }
}

