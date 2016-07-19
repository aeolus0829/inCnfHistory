using System;
using System.Data;
using SAPFunctionsOCX;
using SAPTableFactoryCtrl;
using System.Linq;

namespace defaultProject
{
    public partial class _Default : System.Web.UI.Page
    {
        string filter = "";

        public static T CType<T>(object obj)
        {
            try { return (T)obj; }
            catch { return default(T); }

        }

        protected void btnQry_Click(object sender, EventArgs e)
        {
            //Create a new thread and set the method test() run in this thread
            System.Threading.Thread threads = new System.Threading.Thread(new System.Threading.ThreadStart(callRFC));
            //Set the run mode 'STA'
            threads.SetApartmentState(System.Threading.ApartmentState.STA);
            threads.Start();
            threads.Join();
        }

        private void callRFC()
        {
            Sapcon oLogon = new Sapcon();
            SAPLogonCtrl.Connection oSAPConn = CType<SAPLogonCtrl.Connection>(oLogon.NewConnection());
            
            try
            {
                if (oSAPConn.Logon(0, true))
                {
                    String dateStart = txtDate1.Text.Trim();
                    String dateEnd = txtDate2.Text.Trim();
                    String prodOrderNum = txtProdOrderNum.Text.Trim();
                    String prodOrderControlNum = "1";
                    String salesOrderNum = txtSO.Text.Trim();
                    String salesOrderItemNum = txtSOITEM.Text.Trim();
                    String isSalesOrderEmpty = ""; 

                    SAPFunctionsClass func = new SAPFunctionsClass();
                    func.Connection = oSAPConn;

                    //功能模組名稱
                    IFunction rfcFunc = (IFunction)func.Add("ZPPRFC002");
                    //查詢參數：起始日期
                    IParameter parameter0 = (IParameter)rfcFunc.get_Exports("DATE1");
                    parameter0.Value = dateStart;
                    //查詢參數：結束日期
                    IParameter parameter1 = (IParameter)rfcFunc.get_Exports("DATE2");
                    if (txtDate2.Text == "")
                    parameter1.Value = dateStart;
                    if (txtDate2.Text != "") 
                    parameter1.Value = dateEnd;
                    //查詢參數：工單號碼
                    IParameter parameter2 = (IParameter)rfcFunc.get_Exports("PONUMBER");
                    if (txtProdOrderNum.Text != "") 
                    parameter2.Value = "0000"+prodOrderNum;
                    //查詢參數：工單類別
                    IParameter parameter3 = (IParameter)rfcFunc.get_Exports("CTRLNUMBER");
                    parameter3.Value = prodOrderControlNum;
                    //查詢參數：銷售訂單
                    IParameter parameter4 = (IParameter)rfcFunc.get_Exports("SO");
                    if (txtSO.Text != "")  parameter4.Value = "000"+salesOrderNum;
                    //查詢參數：銷售項次
                    IParameter parameter5 = (IParameter)rfcFunc.get_Exports("SOITEM");
                    if (txtSOITEM.Text != "")  parameter5.Value = "00"+salesOrderItemNum;
                    //查詢參數：刪除空白銷售訂單，設為空白表示不刪除，設為X表示要刪除
                    IParameter parameter6 = (IParameter)rfcFunc.get_Exports("SODEL");
                    parameter6.Value = isSalesOrderEmpty;
                    
                    rfcFunc.Call();

                    Tables tables = (Tables)rfcFunc.Tables;
                    Table ITAB = (Table)tables.get_Item("ITAB");

                    int n = ITAB.RowCount;
                    DataTable dt = new DataTable();
                    for (int i = 1; i <= n; i++)
                    {
                        DataRow dr = dt.NewRow();
                        if (i == 1)
                        {
                            dt.Columns.Add("工單號碼");
                            dt.Columns.Add("工單料號");
                            dt.Columns.Add("作業");
                            dt.Columns.Add("作業短文");
                            dt.Columns.Add("作業數量");
                            dt.Columns.Add("確認良品");
                            dt.Columns.Add("確認廢品");
                            dt.Columns.Add("實際完成日期");
                            dt.Columns.Add("實際完成時間");
                            dt.Columns.Add("整備工時");
                            dt.Columns.Add("機器工時");
                            dt.Columns.Add("人工工時");
                            dt.Columns.Add("工作中心代號");
                            dt.Columns.Add("工作中心名稱");
                            dt.Columns.Add("銷售訂單");                       
                            dt.Columns.Add("銷售項次");
                            dt.Columns.Add("買方");                        
                        }
                        
                        dr["工單號碼"] = ITAB.get_Cell(i, "AUFNR").ToString().TrimStart('0');
                        dr["工單料號"] = ITAB.get_Cell(i, "MATNR").ToString().TrimStart('0');
                        dr["作業"] = ITAB.get_Cell(i, "VORNR").ToString();
                        dr["作業短文"] = ITAB.get_Cell(i, "LTXA1").ToString();
                        dr["作業數量"] = ITAB.get_Cell(i, "SMENG").ToString();
                        dr["確認良品"] = ITAB.get_Cell(i, "GMNGA").ToString();
                        dr["確認廢品"] = ITAB.get_Cell(i, "XMNGA").ToString();
                        if (Convert.ToDateTime(ITAB.get_Cell(i, "IEDD")).ToString("yyyy-MM-dd") != "1899-12-30")
                            dr["實際完成日期"] = Convert.ToDateTime(ITAB.get_Cell(i, "IEDD")).ToString("yyyy-MM-dd");
                        if (Convert.ToDateTime(ITAB.get_Cell(i, "IEDZ")).ToString("HH:mm:ss") != "00:00:00")
                            dr["實際完成時間"] = Convert.ToDateTime(ITAB.get_Cell(i, "IEDZ")).ToString("HH:mm:ss");
                        dr["整備工時"] = ITAB.get_Cell(i, "ISM01").ToString();
                        dr["機器工時"] = ITAB.get_Cell(i, "ISM02").ToString();
                        dr["人工工時"] = ITAB.get_Cell(i, "ISM03").ToString();
                        dr["工作中心代號"] = ITAB.get_Cell(i, "ARBPL").ToString();
                        dr["工作中心名稱"] = ITAB.get_Cell(i, "KTEXT").ToString();                      
                        dr["銷售訂單"] = ITAB.get_Cell(i, "KDAUF").ToString().TrimStart('0');
                        dr["銷售項次"] = ITAB.get_Cell(i, "KDPOS").ToString().TrimStart('0');
                        dr["買方"] = ITAB.get_Cell(i, "SORTL").ToString();

                        dt.Rows.Add(dr);
                    }

                    DataTable dtRmData = removeRows(dt);

                    dtRmData.AcceptChanges();

                    gvData.DataSource = dtRmData;
                    gvData.DataBind();
                    btnConvert.Visible = true;
                }
                else
                {
                    lblMsg.Text = "Logon Fail";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private DataTable removeRows(DataTable dt)
        {
            switch (rblDept.SelectedValue)
            {
                case "0":  //製造以外刪掉，留A00? U00?
                    dt.Rows.Cast<DataRow>().Where(r =>
                    (!r.ItemArray[12].ToString().Contains("A")) ||
                    (!r.ItemArray[12].ToString().Contains("U"))
                    ).ToList().ForEach(r => r.Delete());
                    break;
                case "1":  //出貨以外刪掉，留P00?
                    dt.Rows.Cast<DataRow>().Where(r =>
                    !r.ItemArray[12].ToString().Contains("P")).ToList().ForEach(r => r.Delete());
                    break;
                case "2":  //加工以外刪掉，留L00? S??? B???
                    dt.Rows.Cast<DataRow>().Where(r =>
                    (r.ItemArray[12].ToString().Contains("A")) ||
                    (r.ItemArray[12].ToString().Contains("U")) ||
                    (r.ItemArray[12].ToString().Contains("P"))
                    ).ToList().ForEach(r => r.Delete());
                    break;
            }
            return dt;
        }

        protected void btnClr_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnDate1_Click(object sender, EventArgs e)
        {
            if (!cldrDate1.Visible) cldrDate1.Visible = true;
            else cldrDate1.Visible = false;
        }
        protected void btnDate2_Click(object sender, EventArgs e)
        {
            if (!cldrDate2.Visible) cldrDate2.Visible = true;
            else cldrDate2.Visible = false;
        }
        protected void cldrDate1_SelectionChanged(object sender, EventArgs e)
        {
            txtDate1.Text = cldrDate1.SelectedDate.ToString("yyyyMMdd");
            cldrDate1.Visible = false;
        }
        protected void cldrDate2_SelectionChanged(object sender, EventArgs e)
        {
            txtDate2.Text = cldrDate2.SelectedDate.ToString("yyyyMMdd");
            cldrDate2.Visible = false;
        }
        protected void btnConvert_Click(object sender, EventArgs e)
        {
            
            ExportExcel.ExportToExcel(gvData);
                       
        }
       
}
}
