using System;
using System.Data;
using System.Windows.Forms;

namespace AspNetCore.DynaX.Myrmec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HexInfoList.AutoGenerateColumns = false;
            HexInfoList.DataSource = null;
            HexInfoList.SelectionMode = DataGridViewSelectionMode.CellSelect;
            HexInfoList.MultiSelect = false;
        }

        private void FileSelecter_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                InitialDirectory = "c:\\",
                Filter = "所有文件（*.*）|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            var hexTable = new DataTable();
            hexTable.Columns.Add("FileName", typeof(string));
            hexTable.Columns.Add("FileHex", typeof(string));
            foreach (var fileInfo in openFileDialog.FileNames)
            {
                var fileName = fileInfo.FileFullName();
                var fileBytes = fileInfo.ToFileBytes(20);
                hexTable.Rows.Add(fileName, "[" + fileBytes.ToStr() + "]");
            }
            HexInfoList.DataSource = hexTable;
            HexInfoListClear.Enabled = true;
        }

        private void HexInfoList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != 1) return;
            //若行已是选中状态就不再进行设置
            if (HexInfoList.Rows[e.RowIndex].Selected == false)
            {
                HexInfoList.ClearSelection();
                HexInfoList.Rows[e.RowIndex].Selected = true;
            }
            //只选中一行时设置活动单元格
            if (HexInfoList.SelectedRows.Count == 1)
            {
                HexInfoList.CurrentCell = HexInfoList.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            //弹出操作菜单
            CellContextMenu.Show(MousePosition.X, MousePosition.Y);
        }

        private void CopyCellData_Click(object sender, EventArgs e)
        {
            var currInfo = HexInfoList.CurrentCell?.Value.ToSafeString().TrimStart('[').TrimEnd(']');
            if (!string.IsNullOrWhiteSpace(currInfo))
            {
                Clipboard.SetDataObject(currInfo);
            }
        }

        private void HexInfoListClear_Click(object sender, EventArgs e)
        {
            HexInfoList.DataSource = null;
            HexInfoListClear.Enabled = false;
        }
    }
}
