namespace GameLauncher
{
    partial class MainWindow
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.startBtn = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.versionlabel = new System.Windows.Forms.Label();
            this.serverversionlbl = new System.Windows.Forms.Label();
            this.fireBaseStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(163, 390);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(679, 27);
            this.progressBar2.TabIndex = 0;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(12, 390);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(131, 27);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "Sprawdz aktualizacje";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl1.Location = new System.Drawing.Point(342, 366);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(64, 21);
            this.lbl1.TabIndex = 2;
            this.lbl1.Text = "Gotowy";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // versionlabel
            // 
            this.versionlabel.AutoSize = true;
            this.versionlabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.versionlabel.Location = new System.Drawing.Point(17, 9);
            this.versionlabel.Name = "versionlabel";
            this.versionlabel.Size = new System.Drawing.Size(133, 21);
            this.versionlabel.TabIndex = 3;
            this.versionlabel.Text = "Aktualna wersja: -";
            // 
            // serverversionlbl
            // 
            this.serverversionlbl.AutoSize = true;
            this.serverversionlbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.serverversionlbl.Location = new System.Drawing.Point(17, 30);
            this.serverversionlbl.Name = "serverversionlbl";
            this.serverversionlbl.Size = new System.Drawing.Size(157, 21);
            this.serverversionlbl.TabIndex = 4;
            this.serverversionlbl.Text = "Wersja na serwerze: -";
            // 
            // fireBaseStatusLabel
            // 
            this.fireBaseStatusLabel.AutoSize = true;
            this.fireBaseStatusLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fireBaseStatusLabel.Location = new System.Drawing.Point(17, 51);
            this.fireBaseStatusLabel.Name = "fireBaseStatusLabel";
            this.fireBaseStatusLabel.Size = new System.Drawing.Size(126, 21);
            this.fireBaseStatusLabel.TabIndex = 5;
            this.fireBaseStatusLabel.Text = "Firebase status: -";
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(854, 429);
            this.Controls.Add(this.fireBaseStatusLabel);
            this.Controls.Add(this.serverversionlbl);
            this.Controls.Add(this.versionlabel);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.progressBar2);
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label versionlabel;
        private System.Windows.Forms.Label serverversionlbl;
        private System.Windows.Forms.Label fireBaseStatusLabel;
    }
}

