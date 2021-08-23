using System;

namespace TestApp
{
    partial class GameLauncher
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.startBtn = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.versionlabel = new System.Windows.Forms.Label();
            this.serverversionlbl = new System.Windows.Forms.Label();
            this.firestatus = new System.Windows.Forms.Label();
            this.betaLabel = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(-1, 390);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(843, 27);
            this.progressBar.TabIndex = 0;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(357, 357);
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
            this.lbl1.Location = new System.Drawing.Point(393, 333);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(64, 21);
            this.lbl1.TabIndex = 2;
            this.lbl1.Text = "Gotowy";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // versionlabel
            // 
            this.versionlabel.AutoSize = true;
            this.versionlabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.versionlabel.Location = new System.Drawing.Point(12, 13);
            this.versionlabel.Name = "versionlabel";
            this.versionlabel.Size = new System.Drawing.Size(101, 15);
            this.versionlabel.TabIndex = 3;
            this.versionlabel.Text = "Aktualna wersja: -";
            // 
            // serverversionlbl
            // 
            this.serverversionlbl.AutoSize = true;
            this.serverversionlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.serverversionlbl.Location = new System.Drawing.Point(12, 28);
            this.serverversionlbl.Name = "serverversionlbl";
            this.serverversionlbl.Size = new System.Drawing.Size(117, 15);
            this.serverversionlbl.TabIndex = 4;
            this.serverversionlbl.Text = "Wersja na serwerze: -";
            // 
            // firestatus
            // 
            this.firestatus.AutoSize = true;
            this.firestatus.Location = new System.Drawing.Point(12, 43);
            this.firestatus.Name = "firestatus";
            this.firestatus.Size = new System.Drawing.Size(92, 14);
            this.firestatus.TabIndex = 5;
            this.firestatus.Text = "Firebase status: -";
            // 
            // betaLabel
            // 
            this.betaLabel.AutoSize = true;
            this.betaLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.betaLabel.ForeColor = System.Drawing.Color.Red;
            this.betaLabel.Location = new System.Drawing.Point(679, -1);
            this.betaLabel.Name = "betaLabel";
            this.betaLabel.Size = new System.Drawing.Size(163, 29);
            this.betaLabel.TabIndex = 6;
            this.betaLabel.Text = "Beta version!";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVersion.Location = new System.Drawing.Point(12, 371);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(48, 16);
            this.lblVersion.TabIndex = 8;
            this.lblVersion.Text = "0.0.0.0";
            // 
            // GameLauncher
            // 
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(840, 415);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.betaLabel);
            this.Controls.Add(this.firestatus);
            this.Controls.Add(this.serverversionlbl);
            this.Controls.Add(this.versionlabel);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.progressBar);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "GameLauncher";
<<<<<<< HEAD:TestApp/GameLauncher.Designer.cs
            this.Text = "Game Launcher";
            this.Load += new System.EventHandler(this.GameLauncher_Load);
=======
>>>>>>> parent of efaf55b (hehe):TestApp/Form1.Designer.cs
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label versionlabel;
        private System.Windows.Forms.Label serverversionlbl;
        private System.Windows.Forms.Label firestatus;
        private System.Windows.Forms.Label betaLabel;
        private System.Windows.Forms.Label lblVersion;
    }
}

