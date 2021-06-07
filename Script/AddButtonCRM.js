function contact_onload() {
	// update record in CRM (RO Case)
	Xrm.Page.getAttribute("address1_shippingmethodcode").addOnChange(updateShippingMethod);

	// new page button
	chatmessage_onload();
}

function updateShippingMethod() {
	var api = "https://phoebe.hms-cloud.com:4430/api";

	var formdata = new FormData();
	var text = Xrm.Page.getAttribute("address1_shippingmethodcode").getText();
	var value = Xrm.Page.getAttribute("address1_shippingmethodcode").getValue();

	var data = {
		'Id': 80,
		'CaseId': "DEV000080",
		'Dealer': "Dealer 3",

		//'A_code' : null,
		//'B_code' : "",
		//'C_code' : 'C_code3',

		'ModifiedBy': '999',
		//'StatusCode' : '2',

		//'LevelofProblem' : 'Level2',
		//'CaseTitle' : 'CaseTitle2',
		//'CaseType' : 'Type2',
		'CaseTitle': text,
		'CaseType': value,
	};

	formdata.append('Model', JSON.stringify(data));

	$.ajax({
		type: 'POST',
		url: api + '/rocase/update',
		data: formdata,
		contentType: false,
		processData: false,
		success: function (result) {
			if (result != null) {
				alert(result.Message);
			}
		},
		error: function (xhr, status, error) {
			alert(xhr.responseText);
			alert(status);
			alert(error);
		}
	});
}

function chatmessage_onload() {
	// Web resource: 'WebResource_btn_chatmessage'
	ConvertToButton('WebResource_btn_chatmessage', chatmessage_onclick);
}

function ConvertToButton(fieldname, clickevent) {
	//check if object exists; else return
	var control = Xrm.Page.getControl(fieldname);

	if (control != null) {
		control.getObject().onclick = clickevent;
	}
}

function chatmessage_onclick() {
	var viewportwidth = screen.availWidth;
	var viewportheight = screen.availHeight;
	//window.resizeBy(-300,0);
	window.moveTo(0, 0);
	
	window.open('https://bqas.bigth.com:444//WebResources/hms_html_chatmessage',
		"mywindow",
		"width=750, height=430, left=" + (viewportwidth - 750) + ",top=" + (viewportheight - 430),
	);
}