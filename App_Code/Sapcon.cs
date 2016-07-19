using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SAPLogonCtrl;

/// <summary>ed
/// Summary description for Sapcon
/// </summary>
public class Sapcon : SAPLogonCtrl.SAPLogonControlClass
{
	public  Sapcon()
	{
		//
		// TODO: Add constructor logic here
		//
        // SAPLogonCtrl.SAPLogonControlClass oLogon = new SAPLogonControlClass();
        /*oLogon.ApplicationServer = "192.168.0.16";
        oLogon.Client = "800";
        oLogon.Language = "ZF";
        oLogon.User = "DDIC";
        oLogon.Password = "Ubn3dx";
        oLogon.SystemNumber = Int32.Parse("00");*/

        this.ApplicationServer = "192.168.0.16";
        this.Client = "800";
        this.Language = "ZF";
        this.User = "DDIC";
        this.Password = "Ubn3dx";
        //this.Password = "Admin12-1";
        this.SystemNumber = Int32.Parse("00");
        
	}
}