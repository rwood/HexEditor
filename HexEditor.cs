using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;

namespace HexEditor
{
	/// <summary>
	/// Simple HexEditor written for cs3400 by RogerWood
	/// </summary>
	public class HexEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid HexTable;
		private System.Windows.Forms.DataGrid CharTable;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button OpenFile;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private long currentByte;
		private long maxByte;
		private string fname;
		private Stream fStream;
		private DataTable dataTableHex;
		private System.Windows.Forms.Button PgUp;
		private System.Windows.Forms.Button PgDn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label fnameOut;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label fileSizeOut;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label currentByteOut;
		private System.Windows.Forms.Button Beginning;
		private System.Windows.Forms.Button End;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox SkipTo;
		private System.Windows.Forms.TextBox SkipDown;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox SkipUp;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox SearchFor;
		private System.Windows.Forms.Button SearchGo;
		private System.Windows.Forms.TextBox ModifyText;
		private System.Windows.Forms.Button ModifyGo;
		private DataTable dataTableChar;
		private string CommandLineArgs;


		public HexEditor(string[] CLArgs)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			currentByte = 0;
			maxByte = 0;
			if(CLArgs.Length != 0)
				CommandLineArgs = CLArgs[0];
			else
				CommandLineArgs = "";
			dataTableHex = new DataTable("hexs");
			dataTableChar= new DataTable("chars");
			for(int i = 0; i < 10; i++)
			{
				dataTableHex.Columns.Add(i.ToString(),typeof(string));
				dataTableChar.Columns.Add(i.ToString(),typeof(string));
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.HexTable = new System.Windows.Forms.DataGrid();
			this.CharTable = new System.Windows.Forms.DataGrid();
			this.OpenFile = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.PgUp = new System.Windows.Forms.Button();
			this.PgDn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.fnameOut = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.fileSizeOut = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.currentByteOut = new System.Windows.Forms.Label();
			this.Beginning = new System.Windows.Forms.Button();
			this.End = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.SkipTo = new System.Windows.Forms.TextBox();
			this.SkipDown = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.SkipUp = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.SearchFor = new System.Windows.Forms.TextBox();
			this.SearchGo = new System.Windows.Forms.Button();
			this.ModifyText = new System.Windows.Forms.TextBox();
			this.ModifyGo = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.HexTable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CharTable)).BeginInit();
			this.SuspendLayout();
			// 
			// HexTable
			// 
			this.HexTable.AllowSorting = false;
			this.HexTable.CaptionVisible = false;
			this.HexTable.DataMember = "";
			this.HexTable.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.HexTable.Location = new System.Drawing.Point(0, 96);
			this.HexTable.Name = "HexTable";
			this.HexTable.ParentRowsVisible = false;
			this.HexTable.PreferredColumnWidth = 20;
			this.HexTable.ReadOnly = true;
			this.HexTable.RowHeadersVisible = false;
			this.HexTable.Size = new System.Drawing.Size(204, 194);
			this.HexTable.TabIndex = 0;
			this.HexTable.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.HexTable_MouseWheel);
			// 
			// CharTable
			// 
			this.CharTable.AllowSorting = false;
			this.CharTable.CaptionVisible = false;
			this.CharTable.DataMember = "";
			this.CharTable.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.CharTable.Location = new System.Drawing.Point(208, 96);
			this.CharTable.Name = "CharTable";
			this.CharTable.ParentRowsVisible = false;
			this.CharTable.PreferredColumnWidth = 13;
			this.CharTable.ReadOnly = true;
			this.CharTable.RowHeadersVisible = false;
			this.CharTable.Size = new System.Drawing.Size(134, 194);
			this.CharTable.TabIndex = 1;
			this.CharTable.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.CharTable_MouseWheel);
			// 
			// OpenFile
			// 
			this.OpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OpenFile.Location = new System.Drawing.Point(8, 448);
			this.OpenFile.Name = "OpenFile";
			this.OpenFile.Size = new System.Drawing.Size(136, 23);
			this.OpenFile.TabIndex = 2;
			this.OpenFile.Text = "Open a different file";
			this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CloseButton.Location = new System.Drawing.Point(152, 448);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(120, 23);
			this.CloseButton.TabIndex = 3;
			this.CloseButton.Text = "Close HexEditor";
			this.CloseButton.Click += new System.EventHandler(this.Close_Click);
			// 
			// PgUp
			// 
			this.PgUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PgUp.Location = new System.Drawing.Point(88, 304);
			this.PgUp.Name = "PgUp";
			this.PgUp.TabIndex = 4;
			this.PgUp.Text = "PgUp";
			this.PgUp.Click += new System.EventHandler(this.PgUp_Click);
			// 
			// PgDn
			// 
			this.PgDn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PgDn.Location = new System.Drawing.Point(88, 336);
			this.PgDn.Name = "PgDn";
			this.PgDn.TabIndex = 5;
			this.PgDn.Text = "PgDn";
			this.PgDn.Click += new System.EventHandler(this.PgDn_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Filename: ";
			// 
			// fnameOut
			// 
			this.fnameOut.Location = new System.Drawing.Point(64, 8);
			this.fnameOut.Name = "fnameOut";
			this.fnameOut.Size = new System.Drawing.Size(272, 48);
			this.fnameOut.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Filesize:";
			// 
			// fileSizeOut
			// 
			this.fileSizeOut.Location = new System.Drawing.Point(48, 56);
			this.fileSizeOut.Name = "fileSizeOut";
			this.fileSizeOut.Size = new System.Drawing.Size(200, 16);
			this.fileSizeOut.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 16);
			this.label3.TabIndex = 10;
			this.label3.Text = "Current Byte:";
			// 
			// currentByteOut
			// 
			this.currentByteOut.Location = new System.Drawing.Point(80, 72);
			this.currentByteOut.Name = "currentByteOut";
			this.currentByteOut.Size = new System.Drawing.Size(176, 16);
			this.currentByteOut.TabIndex = 11;
			// 
			// Beginning
			// 
			this.Beginning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Beginning.Location = new System.Drawing.Point(8, 304);
			this.Beginning.Name = "Beginning";
			this.Beginning.TabIndex = 13;
			this.Beginning.Text = "Beginning";
			this.Beginning.Click += new System.EventHandler(this.Beginning_Click);
			// 
			// End
			// 
			this.End.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.End.Location = new System.Drawing.Point(8, 336);
			this.End.Name = "End";
			this.End.TabIndex = 14;
			this.End.Text = "End";
			this.End.Click += new System.EventHandler(this.End_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(192, 360);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 16);
			this.label5.TabIndex = 15;
			this.label5.Text = "Go to byte ";
			// 
			// SkipTo
			// 
			this.SkipTo.Location = new System.Drawing.Point(248, 352);
			this.SkipTo.MaxLength = 9;
			this.SkipTo.Name = "SkipTo";
			this.SkipTo.Size = new System.Drawing.Size(64, 20);
			this.SkipTo.TabIndex = 16;
			this.SkipTo.Text = "";
			this.SkipTo.WordWrap = false;
			this.SkipTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SkipTo_KeyDown);
			this.SkipTo.LostFocus += new System.EventHandler(this.SkipTo_LostFocus);
			// 
			// SkipDown
			// 
			this.SkipDown.Location = new System.Drawing.Point(248, 328);
			this.SkipDown.MaxLength = 9;
			this.SkipDown.Name = "SkipDown";
			this.SkipDown.Size = new System.Drawing.Size(64, 20);
			this.SkipDown.TabIndex = 17;
			this.SkipDown.Text = "";
			this.SkipDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SkipDown_KeyDown);
			this.SkipDown.LostFocus += new System.EventHandler(this.SkipDown_LostFocus);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(192, 336);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 16);
			this.label6.TabIndex = 18;
			this.label6.Text = "Go down";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(312, 336);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 16);
			this.label7.TabIndex = 19;
			this.label7.Text = "bytes";
			// 
			// SkipUp
			// 
			this.SkipUp.Location = new System.Drawing.Point(248, 304);
			this.SkipUp.MaxLength = 9;
			this.SkipUp.Name = "SkipUp";
			this.SkipUp.Size = new System.Drawing.Size(64, 20);
			this.SkipUp.TabIndex = 20;
			this.SkipUp.Text = "";
			this.SkipUp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SkipUp_KeyDown);
			this.SkipUp.LostFocus += new System.EventHandler(this.SkipUp_LostFocus);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(208, 312);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 16);
			this.label8.TabIndex = 21;
			this.label8.Text = "Go up";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(312, 312);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(40, 16);
			this.label9.TabIndex = 22;
			this.label9.Text = "bytes";
			// 
			// SearchFor
			// 
			this.SearchFor.Location = new System.Drawing.Point(8, 384);
			this.SearchFor.Name = "SearchFor";
			this.SearchFor.Size = new System.Drawing.Size(248, 20);
			this.SearchFor.TabIndex = 23;
			this.SearchFor.Text = "Search";
			this.SearchFor.LostFocus += new System.EventHandler(this.SearchFor_LostFocus);
			this.SearchFor.GotFocus += new System.EventHandler(this.SearchFor_GotFocus);
			// 
			// SearchGo
			// 
			this.SearchGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SearchGo.Location = new System.Drawing.Point(264, 384);
			this.SearchGo.Name = "SearchGo";
			this.SearchGo.TabIndex = 24;
			this.SearchGo.Text = "Search";
			this.SearchGo.Click += new System.EventHandler(this.SearchGo_Click);
			// 
			// ModifyText
			// 
			this.ModifyText.Location = new System.Drawing.Point(8, 416);
			this.ModifyText.Name = "ModifyText";
			this.ModifyText.Size = new System.Drawing.Size(248, 20);
			this.ModifyText.TabIndex = 25;
			this.ModifyText.Text = "Overwrite text from current byte";
			this.ModifyText.GotFocus += new System.EventHandler(this.ModifyText_GotFocus);
			// 
			// ModifyGo
			// 
			this.ModifyGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ModifyGo.Location = new System.Drawing.Point(264, 416);
			this.ModifyGo.Name = "ModifyGo";
			this.ModifyGo.TabIndex = 26;
			this.ModifyGo.Text = "Modify";
			this.ModifyGo.Click += new System.EventHandler(this.ModifyGo_Click);
			// 
			// HexEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(346, 479);
			this.Controls.Add(this.ModifyGo);
			this.Controls.Add(this.ModifyText);
			this.Controls.Add(this.SearchGo);
			this.Controls.Add(this.SearchFor);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.SkipUp);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.SkipDown);
			this.Controls.Add(this.SkipTo);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.End);
			this.Controls.Add(this.Beginning);
			this.Controls.Add(this.currentByteOut);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.fileSizeOut);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.fnameOut);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.PgDn);
			this.Controls.Add(this.PgUp);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.OpenFile);
			this.Controls.Add(this.CharTable);
			this.Controls.Add(this.HexTable);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "HexEditor";
			this.Text = "HexEditor by Roger Wood";
			this.Load += new System.EventHandler(this.HexEditor_Load);
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.HexEditor_MouseWheel);
			((System.ComponentModel.ISupportInitialize)(this.HexTable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CharTable)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			Application.Run(new HexEditor(args));
		}

		private void HexEditor_Load(object sender, System.EventArgs e)
		{
			fname = "";
			if(CommandLineArgs.Length != 0)
				HexEditorInitStream(CommandLineArgs);
			else
			{
				if(this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					HexEditorInitStream(this.openFileDialog1.FileName);
				else
					Close();
			}		
		}

		private void HexEditorInitStream(string file)
		{
			fname = file;
			fStream = File.Open(fname, FileMode.Open, FileAccess.ReadWrite);
			fnameOut.Text = fname;
			currentByte = 0;
			SetMaxByte();
			ReadFile();
		}

		private void Close_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void OpenFile_Click(object sender, System.EventArgs e)
		{
			fname = "";
			if(this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				HexEditorInitStream(this.openFileDialog1.FileName);
		}

		private void ReadFile()
		{
			fStream.Position = currentByte;
			dataTableHex.Clear();
			dataTableChar.Clear();
			for(int i = 0; i < 10; i++)
			{
				int inTemp = 0;
				DataRow inTempHex = dataTableHex.NewRow();
				DataRow inTempChar = dataTableChar.NewRow();
				for(int n = 0; n < 10; n++)
				{
					inTemp = fStream.ReadByte();
					if(inTemp == -1)
					{
						inTemp = 0;
					}
					inTempHex[n] = String.Format("{0:x}", inTemp);
					inTempChar[n] = Convert.ToChar(inTemp);
				}
				dataTableChar.Rows.Add(inTempChar);
				dataTableHex.Rows.Add(inTempHex);
				inTemp = 0;
				inTempChar = null;
				inTempHex = null;
			}
			currentByteOut.Text = currentByte.ToString();
			HexTable.DataSource = dataTableHex;
			CharTable.DataSource = dataTableChar;
		}

		private void SetMaxByte()
		{
			fStream.Position = 0;
			while(fStream.ReadByte() != -1){}
			maxByte = fStream.Position;
			fileSizeOut.Text = maxByte.ToString() + " bytes";
			fStream.Position = 0;
		}

		private void PgDn_Click(object sender, System.EventArgs e)
		{
			PageDown(100);
		}

		private void PageDown(long BytesToGo)
		{
			if((currentByte + BytesToGo) <= maxByte)
				currentByte += BytesToGo;
			else
				currentByte = maxByte;
			fStream.Position = currentByte;
			currentByteOut.Text = currentByte.ToString();
			ReadFile();
		}

		private void PgUp_Click(object sender, System.EventArgs e)
		{
			PageUp(100);	
		}

		private void PageUp(long BytesToGo)
		{
			if((currentByte - BytesToGo) < 0)
				currentByte = 0;
			else
				currentByte -= BytesToGo;
			fStream.Position = currentByte;
			currentByteOut.Text = currentByte.ToString();
			ReadFile();
		}

		private void Beginning_Click(object sender, System.EventArgs e)
		{
			PageUp(currentByte);
		}

		private void End_Click(object sender, System.EventArgs e)
		{
			if(maxByte < 100)
				PageUp(currentByte);
			else
				PageDown(maxByte - 100);
		}

		private void SkipUp_LostFocus(object sender, System.EventArgs e)
		{
			/*if(!System.Text.RegularExpressions.Regex.IsMatch(SkipUp.Text, "^[0-9]"))
				return;
			if((currentByte - Convert.ToInt32(SkipUp.Text)) < 0)
				PageUp(currentByte);
			else
				PageUp(currentByte - Convert.ToInt32(SkipUp.Text));*/
			SkipUp.Text = "";
		}

		private void SkipDown_LostFocus(object sender, System.EventArgs e)
		{
			/*if(!System.Text.RegularExpressions.Regex.IsMatch(SkipDown.Text, "^[0-9]"))
				return;
			if((currentByte + Convert.ToInt32(SkipDown.Text)) <= maxByte)
				currentByte += Convert.ToInt32(SkipDown.Text);
			else
				currentByte = maxByte;
			fStream.Position = currentByte;
			currentByteOut.Text = currentByte.ToString();
			ReadFile();*/
			SkipDown.Text = "";
		}

		private void SkipTo_LostFocus(object sender, System.EventArgs e)
		{
			/*if(!System.Text.RegularExpressions.Regex.IsMatch(SkipTo.Text, "^[0-9]"))
				return;
			if(Convert.ToInt32(SkipTo.Text) < 0)
				currentByte = 0;
			else if(Convert.ToInt32(SkipTo.Text) > maxByte)
				currentByte = maxByte;
			else
				currentByte = Convert.ToInt32(SkipTo.Text);
			fStream.Position = currentByte;
			currentByteOut.Text = currentByte.ToString();
			ReadFile();*/
			SkipTo.Text = "";
		}

		private void SearchFor_GotFocus(object sender, System.EventArgs e)
		{
			if(SearchFor.Text == "Search")
				SearchFor.Text = "";
		}

		private void SearchFor_LostFocus(object sender, System.EventArgs e)
		{
			return;
		}

		private void SearchGo_Click(object sender, System.EventArgs e)
		{
			fStream.Position = currentByte+1;
			if(SearchGo.Text == "")
				return;
			bool leave = false;
			long matchByte = 0;
			while(!leave)
			{
				long TempLong = fStream.ReadByte();
				if(TempLong == -1)
				{
					MessageBox.Show("End of file reached.");
					currentByte = maxByte;
					ReadFile();
					return;
				}
				else if(TempLong == Convert.ToInt32(SearchFor.Text[0]))
				{
					matchByte = fStream.Position-1;
					bool match = true;
					for(int i = 1; i < SearchFor.Text.Length; i++)
					{
						TempLong = fStream.ReadByte();
						if(TempLong == -1)
						{
							MessageBox.Show("End of file reached.");
							currentByte = maxByte;
							ReadFile();
							return;
						}
						if(TempLong != Convert.ToInt32(SearchFor.Text[i]))
						{
							match = false;
							break;
						}
					}
					if(match)
					{
						currentByte = matchByte;
						ReadFile();
						return;
					}
				}
				else
					continue;
			}
		}

		private void ModifyGo_Click(object sender, System.EventArgs e)
		{
			fStream.Position = currentByte;
			fStream.Write(ConvertStringToByteArray(ModifyText.Text), 0, ModifyText.Text.Length);
			if(fStream.Position > maxByte)
				SetMaxByte();
			fStream.Position = currentByte;
			ReadFile();

		}

		public static byte[] ConvertStringToByteArray(string stringToConvert)
		{
			return (new ASCIIEncoding()).GetBytes(stringToConvert);
		}

		private void ModifyText_GotFocus(object sender, System.EventArgs e)
		{
			if(ModifyText.Text == "Overwrite text from current byte")
				ModifyText.Text = "";
		}

			private void HexTable_MouseWheel(object sender, MouseEventArgs e)
		{
			if(e.Delta < 0)
			{
				PageDown(100);
			}
			else if(e.Delta > 0)
			{
				PageUp(100);
			}
		}

		private void CharTable_MouseWheel(object sender, MouseEventArgs e)
		{
			if(e.Delta < 0)
			{
				PageDown(100);
			}
			else if(e.Delta > 0)
			{
				PageUp(100);
			}
		}

		private void HexEditor_MouseWheel(object sender, MouseEventArgs e)
		{
			if(e.Delta < 0)
			{
				PageDown(100);
			}
			else if(e.Delta > 0)
			{
				PageUp(100);
			}
		}

		private void SkipUp_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				if(!System.Text.RegularExpressions.Regex.IsMatch(SkipUp.Text, "^[0-9]"))
					return;
				if((currentByte - Convert.ToInt32(SkipUp.Text)) < 0)
					currentByte = 0;
				else
					currentByte -= Convert.ToInt32(SkipUp.Text);
				fStream.Position = currentByte;
				currentByteOut.Text = currentByte.ToString();
				ReadFile();
				//SkipUp.Text = "";
			}
		}

		private void SkipDown_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				if(!System.Text.RegularExpressions.Regex.IsMatch(SkipDown.Text, "^[0-9]"))
					return;
				if((currentByte + Convert.ToInt32(SkipDown.Text)) <= maxByte)
					currentByte += Convert.ToInt32(SkipDown.Text);
				else
					currentByte = maxByte;
				fStream.Position = currentByte;
				currentByteOut.Text = currentByte.ToString();
				ReadFile();
				//SkipDown.Text = "";
			}
		}

		private void SkipTo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				if(!System.Text.RegularExpressions.Regex.IsMatch(SkipTo.Text, "^[0-9]"))
					return;
				if(Convert.ToInt32(SkipTo.Text) < 0)
					currentByte = 0;
				else if(Convert.ToInt32(SkipTo.Text) > maxByte)
					currentByte = maxByte;
				else
					currentByte = Convert.ToInt32(SkipTo.Text);
				fStream.Position = currentByte;
				currentByteOut.Text = currentByte.ToString();
				ReadFile();
				//SkipTo.Text = "";
			}
		}

	}
}
