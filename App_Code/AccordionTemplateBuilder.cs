using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for AccordionTemplateBuilder
/// </summary>
public class AccordionTemplateBuilder : TemplateControl, ITemplate
{
    private string _template;

	public AccordionTemplateBuilder(string template)
	{
        this._template = template;		
	}

    public void InstantiateIn(Control container)
    {
        LiteralControl lc = new LiteralControl(this._template);
        container.Controls.Add(lc);
    }
}
