using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;

namespace Nhom3
{
    public partial class frmHH : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string str, sql;
        Boolean addnewflag = false;
        public frmHH()
        {
            InitializeComponent();
        }
        public void NapCT()
        {
            int i = grdDMHH.CurrentRow.Index;
            txtManhom.Text = grdDMHH.Rows[i].Cells["Manhom"].Value.ToString();
            txtMaHH.Text = grdDMHH.Rows[i].Cells["MaHH"].Value.ToString();
            txtTenHH.Text = grdDMHH.Rows[i].Cells["TenHH"].Value.ToString();
            txtDvt.Text = grdDMHH.Rows[i].Cells["Dvt"].Value.ToString();
            txtDgVnd.Text = grdDMHH.Rows[i].Cells["DgVnd"].Value.ToString();
            txtSanxuat.Text = grdDMHH.Rows[i].Cells["Sanxuat"].Value.ToString();
        }
        // Sự kiện khi người dùng click chuột vào một dòng trên lưới dữ liệu
        private void frmHH_Load(object sender, EventArgs e)
        {
           str= "Data Source = LAPTOP-8EI4770R; " +
                "Initial Catalog = qlbh;" +
                "Integrated Security = True; " +
                "Encrypt = False";
              conn.ConnectionString = str;
            conn.Open();
            sql = "select Manhom, MaHH, TenHH, Dvt, DgVnd, Sanxuat from tblDMHH";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdDMHH.DataSource = dt;
            NapCT();
        }

        private void grdDMHH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void btnPrv_Click(object sender, EventArgs e)
        {
            int i = grdDMHH.CurrentRow.Index;
            if (i>0)
            {
                grdDMHH.CurrentCell = grdDMHH[0, i - 1];
                NapCT();
            }    
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            grdDMHH.CurrentCell = grdDMHH[0, 0];
            NapCT();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int i = grdDMHH.CurrentRow.Index;
            if (i < grdDMHH.RowCount - 1)
            {
                grdDMHH.CurrentCell = grdDMHH[0, i + 1];
                NapCT();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            grdDMHH.CurrentCell = grdDMHH[0, grdDMHH.RowCount - 1];
            NapCT();    
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            sql= "select distinct " + comTruong.Text + " from tblDMHH";
            da = new SqlDataAdapter(sql, conn);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            comGT.DataSource = dt1;
            comGT.DisplayMember = comTruong.Text;
            comGT.ValueMember = comTruong.Text;

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                sql = "select Manhom, MaHH, TenHH, Dvt, DgVnd, Sanxuat from tblDMHH" +
                    " where " + comTruong.Text + "=N'" + comGT.Text + "'";
                da = new SqlDataAdapter(sql, conn);
                dt.Clear();
                da.Fill(dt);
                grdDMHH.DataSource = dt;
                NapCT();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Có lỗi cần xử lý! " + ex.Message);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            NapLai();

        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn hãy sửa thông tin hàng hóa vào các ô lưới rồi nhấn nút Cập nhật!");

        }
        // Sự kiện khi người dùng nhấn nút Cập nhật
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu bắt buộc
            if (string.IsNullOrWhiteSpace(txtMaHH.Text))
            {
                MessageBox.Show("Mã hàng hóa không được để trống!");
                return;
            }

            try
            {
                // Nếu đang ở chế độ thêm mới
                if (addnewflag)
                {
                    // Chuẩn bị câu lệnh Insert
                    sql = "INSERT INTO tblDMHH (Manhom, MaHH, TenHH, Dvt, DgVnd, Sanxuat) " +
                          "VALUES (N'" + txtManhom.Text + "', N'" + txtMaHH.Text + "', N'" +
                          txtTenHH.Text + "', N'" + txtDvt.Text + "', " +
                          (string.IsNullOrEmpty(txtDgVnd.Text) ? "0" : txtDgVnd.Text) + ", N'" +
                          txtSanxuat.Text + "')";

                    // Thực hiện câu lệnh Insert
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Đã thêm hàng hóa mới!");
                }
                else // Nếu không phải thêm mới → Sửa thông tin
                {
                    // Chuẩn bị câu lệnh Update dựa vào MaHH
                    sql = "UPDATE tblDMHH SET " +
                          "Manhom = N'" + txtManhom.Text + "', " +
                          "TenHH = N'" + txtTenHH.Text + "', " +
                          "Dvt = N'" + txtDvt.Text + "', " +
                          "DgVnd = " + (string.IsNullOrEmpty(txtDgVnd.Text) ? "0" : txtDgVnd.Text) + ", " +
                          "Sanxuat = N'" + txtSanxuat.Text + "' " +
                          "WHERE MaHH = N'" + txtMaHH.Text + "'";

                    // Thực hiện câu lệnh Update
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                        MessageBox.Show("Đã cập nhật thông tin hàng hóa!");
                    else
                        MessageBox.Show("Không tìm thấy hàng hóa để cập nhật!");
                }

                // Nạp lại dữ liệu
                NapLai();
                addnewflag = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            // Thực hiện lệnh Delete để xóa một hàng hóa
            // Hiện thông báo xác nhận việc xóa hàng hóa
            // Nếu người dùng đồng ý thì mới xóa

            if (MessageBox.Show("Bạn có chắc chắn xóa hàng hóa này không?", "Hỏi xóa",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Thực hiện lệnh SQL
                sql = "Delete from tblDMHH where MaHH=N'" + txtMaHH.Text + "'";
                // Thực hiện câu lệnh SQL
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                // Cập nhật lại lưới dữ liệu
                int i = grdDMHH.CurrentRow.Index;
                grdDMHH.Rows.RemoveAt(i);
                NapCT();
                MessageBox.Show("Đã xóa hàng hóa");
                
            }
        }
        // Sự kiện khi người dùng nhấn phím Enter
        private void frmHH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }    
        }

        private void btnAddnew_Click_1(object sender, EventArgs e)
        {
            DataRow newRow = dt.NewRow();
            newRow["Manhom"] = "";
            newRow["MaHH"] = "";
            newRow["TenHH"] = "";
            newRow["Dvt"] = "";
            newRow["DgVnd"] = 0;
            newRow["Sanxuat"] = "";
            dt.Rows.Add(newRow);
            int i = grdDMHH.Rows.Count - 1;
            grdDMHH.CurrentCell = grdDMHH[0, i];
            NapCT();
            MessageBox.Show("Bạn hãy nhập thông tin hàng hóa vào các ô text rồi nhấn nút cập nhật!");
            txtManhom.Focus();
            addnewflag = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXuatDL_Click(object sender, EventArgs e)
        {
            rptDMHH rpt = new rptDMHH();
            sql = "select Manhom, MaHH, TenHH, Dvt, DgVnd, Sanxuat from tblDMHH" +
                    " where " + comTruong.Text + "=N'" + comGT.Text + "'";
            da = new SqlDataAdapter(sql, conn);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            rpt.DataSource = dt1;
            rpt.lblDKLoc.Text = "Điều kiện lọc: " + comTruong.Text + " = " + comGT.Text;
            rpt.lblPrintDate.Text = string.Format("Hà Nội, ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            rpt.ShowPreview();
        }


        // Hàm nạp lại dữ liệu
        private void NapLai ()
        {
            try
            {
                sql = "select Manhom, MaHH, TenHH, Dvt, DgVnd, Sanxuat from tblDMHH";
                da = new SqlDataAdapter(sql, conn);
                dt.Clear();
                da.Fill(dt);
                grdDMHH.DataSource = dt;
                NapCT();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Có lỗi cần xử lý! " + ex.Message);
            }
        }
    }
}
