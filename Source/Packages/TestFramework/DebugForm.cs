using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
namespace TestFramework
{
    public partial class DebugForm : Form
    {
        public string text;
        public object Value;
        public Dictionary<string, object> Values = new();
        public DebugForm(string name, object value)
        {
            InitializeComponent();
            Value = value;
            if (value == null) return;
            text = name;
            Text = $"{name}={value}";
            Type type = value.GetType();
            NameColumn.Width = TypeColumn.Width = Width / 5;
            ValueColumn.Width = 3 * Width / 5;
            foreach (FieldInfo fi in type.GetRuntimeFields())
                listView1.Items.Add(new ListViewItem(fi.Name)
                {
                    SubItems = { fi.FieldType.Name, $"{Values[fi.Name] = fi.GetValue(value)}" }
                });
            foreach (PropertyInfo runtimeProperty in type.GetRuntimeProperties())
            {
                ListViewItem listViewItem2 = new(runtimeProperty.Name)
                {
                    SubItems = { runtimeProperty.PropertyType.Name }
                };
                try
                {
                    listViewItem2.SubItems.Add($"{Values[runtimeProperty.Name] = runtimeProperty.GetValue(value)}");
                }
                catch (Exception ex)
                {
                    listViewItem2.SubItems.Add(ex.ToString());
                }
                listView1.Items.Add(listViewItem2);
            }
            foreach (MethodInfo runtimeMethod in type.GetRuntimeMethods())
                listView1.Items.Add(new ListViewItem(runtimeMethod.Name)
                {
                    SubItems = { "Method", $"{Values[runtimeMethod.Name] = runtimeMethod}" }
                });
            int num2 = 0;
            if (value is IEnumerable)
                foreach (object item in value as IEnumerable)
                    if (item != null)
                    {
                        string key = $"[{num2++}]";
                        listView1.Items.Add(new ListViewItem(key)
                        {
                            SubItems = { item.GetType().Name, $"{Values[key] = item}" }
                        });
                    }
        }
        public static void Display(string name, object value) => new DebugForm(name, value).ShowDialog();
        private void invokeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;
            ListViewItem listViewItem = listView1.SelectedItems[0];
            if (listViewItem.SubItems[1].Text != "Method")
                return;
            string text = listViewItem.Text;
            MethodInfo methodInfo = Values[text] as MethodInfo;
            if (methodInfo.GetParameters().Length == 0)
            {
                if (methodInfo.IsStatic)
                    Display(methodInfo.DeclaringType.Name + "." + text + "()", methodInfo.Invoke(null, Array.Empty<object>()));
                else
                    Display(this.text + "." + text + "()", methodInfo.Invoke(Value, Array.Empty<object>()));
            }
        }
        private void DebugForm_SizeChanged(object sender, EventArgs e)
        {
            NameColumn.Width = TypeColumn.Width = Width / 5;
            ValueColumn.Width = 3 * Width / 5;
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                string text = listView1.SelectedItems[0].Text;
                Display(this.text + "." + text, Values[text]);
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e) 
            => MessageBox.Show(listView1.SelectedItems[0].SubItems[2].ToString());

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
            => MessageBox.Show(Value.ToString());
    }
}
