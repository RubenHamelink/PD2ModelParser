// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Form1
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PD2ModelParser
{
  public class Form1 : Form
  {
    public static FileManager fm = new FileManager();
    private IContainer components;
    private TextBox textBox1;
    private Button button1;
    private Label label1;
    private Label label2;
    private TextBox textBox2;
    private Label label3;
    private FolderBrowserDialog folderBrowserDialog1;
    private Button button2;
    private Button button3;
    private Button button4;
    private Label label4;
    private ComboBox rootPoint_combobox;
    private CheckBox addNewObjects_checkbox;

    public Form1()
    {
      this.InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Form1.fm = new FileManager();
      StaticStorage.objects_list = new List<string>();
      StaticStorage.rp_id = 0U;
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Diesel Model(*.model)|*.model";
      openFileDialog.CheckFileExists = true;
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        this.textBox1.Text = openFileDialog.FileName;
        Form1.fm.Open(openFileDialog.FileName, this.rootPoint_combobox.Text);
      }
      this.rootPoint_combobox.Items.Clear();
      this.rootPoint_combobox.Items.AddRange((object[]) StaticStorage.objects_list.ToArray());
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (this.textBox2.Text.Length == 0)
      {
        int num1 = (int) MessageBox.Show("Please enter an export file name.", "Error");
      }
      else if (Form1.fm.GenerateNewModel(this.textBox2.Text))
      {
        int num2 = (int) MessageBox.Show("Model generated successfully");
      }
      else
      {
        int num3 = (int) MessageBox.Show("There was an error generating your model");
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.CheckFileExists = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      if (Form1.fm.ImportNewObj(openFileDialog.FileName, this.addNewObjects_checkbox.Checked))
      {
        int num1 = (int) MessageBox.Show("OBJ imported successfully");
      }
      else
      {
        int num2 = (int) MessageBox.Show("Theere was an error importing OBJ");
      }
    }

    private void button4_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.CheckFileExists = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      if (Form1.fm.ImportNewObjPatternUV(openFileDialog.FileName))
      {
        int num1 = (int) MessageBox.Show("Pattern UV imported successfully");
      }
      else
      {
        int num2 = (int) MessageBox.Show("Theere was an error importing OBJ");
      }
    }

    private void rootPoint_combobox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (Form1.fm.updateRP(this.rootPoint_combobox.Text))
      {
        int num1 = (int) MessageBox.Show("Set model root_point successfully");
      }
      else
      {
        int num2 = (int) MessageBox.Show("Failed setting model root_point!");
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.textBox1 = new TextBox();
      this.button1 = new Button();
      this.label1 = new Label();
      this.label2 = new Label();
      this.textBox2 = new TextBox();
      this.label3 = new Label();
      this.folderBrowserDialog1 = new FolderBrowserDialog();
      this.button2 = new Button();
      this.button3 = new Button();
      this.button4 = new Button();
      this.label4 = new Label();
      this.rootPoint_combobox = new ComboBox();
      this.addNewObjects_checkbox = new CheckBox();
      this.SuspendLayout();
      this.textBox1.Location = new Point(71, 6);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(113, 20);
      this.textBox1.TabIndex = 0;
      this.button1.Location = new Point(190, 4);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Browse...";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(53, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Input File:";
      this.label1.Click += new EventHandler(this.label1_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(12, 35);
      this.label2.Name = "label2";
      this.label2.Size = new Size(61, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Output File:";
      this.label2.Click += new EventHandler(this.label2_Click);
      this.textBox2.Location = new Point(71, 32);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new Size(113, 20);
      this.textBox2.TabIndex = 3;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(336, 95);
      this.label3.Name = "label3";
      this.label3.Size = new Size(180, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Credit to PoueT and I am not a spy...";
      this.button2.Location = new Point(190, 30);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 23);
      this.button2.TabIndex = 7;
      this.button2.Text = "Export";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button3.Location = new Point(135, 59);
      this.button3.Name = "button3";
      this.button3.Size = new Size(130, 23);
      this.button3.TabIndex = 8;
      this.button3.Text = "Import Model (.obj)";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button4.Location = new Point(135, 88);
      this.button4.Name = "button4";
      this.button4.Size = new Size(130, 23);
      this.button4.TabIndex = 9;
      this.button4.Text = "Import Model (2nd UV)";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.label4.AutoSize = true;
      this.label4.Location = new Point(271, 9);
      this.label4.Name = "label4";
      this.label4.Size = new Size(59, 13);
      this.label4.TabIndex = 10;
      this.label4.Text = "Root point:";
      this.rootPoint_combobox.FormattingEnabled = true;
      this.rootPoint_combobox.Location = new Point(336, 6);
      this.rootPoint_combobox.Name = "rootPoint_combobox";
      this.rootPoint_combobox.Size = new Size(180, 21);
      this.rootPoint_combobox.TabIndex = 11;
      this.rootPoint_combobox.SelectedIndexChanged += new EventHandler(this.rootPoint_combobox_SelectedIndexChanged);
      this.addNewObjects_checkbox.AutoSize = true;
      this.addNewObjects_checkbox.Location = new Point(15, 63);
      this.addNewObjects_checkbox.Name = "addNewObjects_checkbox";
      this.addNewObjects_checkbox.Size = new Size(105, 17);
      this.addNewObjects_checkbox.TabIndex = 12;
      this.addNewObjects_checkbox.Text = "Add new objects";
      this.addNewObjects_checkbox.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(527, 116);
      this.Controls.Add((Control) this.addNewObjects_checkbox);
      this.Controls.Add((Control) this.rootPoint_combobox);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.button4);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.textBox1);
      this.MaximumSize = new Size(543, 155);
      this.MinimumSize = new Size(543, 155);
      this.Name = nameof (Form1);
      this.ShowIcon = false;
      this.Text = "Diesel Model Tool v1.03";
      this.Load += new EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
