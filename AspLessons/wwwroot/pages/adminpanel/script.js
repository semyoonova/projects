document.addEventListener('DOMContentLoaded', () => {

    async function getMasters() {
        await fetch(`/getmasters`, {
            method: "GET",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
        })
        .then(response => {
            return response.json(); 
        })
        .then(data => {
            for (let i = 0; i < data.masters.length; i++) {
                let obj = data.masters[i];
                chooseMasterOptions.innerHTML += "<option value=" + obj + ">" + obj + "</option>";
                chooseMasterOptionsToDelete.innerHTML += "<option value=" + obj + ">" + obj + "</option>";
                chooseMasterOptionsToDeleteFromMaster.innerHTML += "<option value=" + obj + ">" + obj + "</option>";
                chooseMasterOptionsToAddWorkhours.innerHTML += "<option value=" + obj + ">" + obj + "</option>";

            };
        });
    }

    async function getService() {
        await fetch(`/getservices`, {
            method: "GET",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
        })
            .then(response => {
                return response.json();
            })
            .then(data => {
                for (let i = 0; i < data.services.length; i++) {
                    let obj = data.services[i];
                    chooseServiceOptions.innerHTML += "<option value=" + obj + ">" + obj + "</option>";
                    chooseServiceOptionsToChangePrice.innerHTML += "<option value=" + obj + ">" + obj + "</option>";
                    chooseServiceOptionsToDelete.innerHTML += "<option value=" + obj + ">" + obj + "</option>";
                    chooseServiceOptionsToDeleteFromMaster.innerHTML += "<option value=" + obj + ">" + obj + "</option>";
                };
            });
    }

   
    const addServiceButton = document.getElementById('addservice');
    const addMasterButton = document.getElementById('addmaster');
    const addServiceToMasterButton = document.getElementById('addservicetomaster');
    const ChangePriceButton = document.getElementById('changeprice');
    const DeleteMasterButton = document.getElementById('deletemaster');
    const DeleteServiceButton = document.getElementById('deleteservice');
    const DeleteServiceFromMasterButton = document.getElementById('deleteservicefrommaster');
    const AddWorkhoursButton = document.getElementById('addworkhours');


    
    const chooseMasterOptions = document.getElementById('masteroptions');
    const chooseServiceOptions = document.getElementById('serviceoptions');
    const chooseServiceOptionsToChangePrice = document.getElementById('servicetochangeprice');
    const chooseMasterOptionsToDelete = document.getElementById('deletemasteroptions');
    const chooseServiceOptionsToDelete = document.getElementById('deleteserviceoptions');
    const chooseServiceOptionsToDeleteFromMaster = document.getElementById('deleteservicefrommasteroptionsmaster');
    const chooseMasterOptionsToDeleteFromMaster = document.getElementById('deleteservicefrommasteroptionsservice');
    const chooseMasterOptionsToAddWorkhours = document.getElementById('addworkhoursoptionsmaster');


    getMasters();
    getService();

    function addServiceValidate() {
        if (!servicename.validity.valid) {
            document.getElementById("lineinfo").innerHTML = "Название услуги не должно быть меньше 2 символов" + '<br />';
            return false;
        }
        if (!price.validity.valid) {
            document.getElementById("lineinfo").innerHTML = "Цена не должна быть пустой" + '<br />';
            return false;
        }
        if (!duration.validity.valid) {
            document.getElementById("lineinfo").innerHTML = "Длительность не должна быть пустой" + '<br />';
            return false;
        }
        return true;
    }

    function ChangePriceValidate() {
        if (!pricetochange.validity.valid) {
            document.getElementById("lineinfochangeprice").innerHTML = "Цена не должна быть пустой" + '<br />';
            return false;
        }
        return true;
    }

    function addMasterValidate() {
        if (!mastername.validity.valid) {
            document.getElementById("lineinfoaddmaster").innerHTML = "Имя мастера не должно быть меньше 2 символов" + '<br />';
            return false;
        }
        return true;
    }

    addServiceButton.addEventListener('click', async () => {
        document.getElementById("lineinfo").innerHTML = "";
        let servicename = document.getElementById("servicename");
        let price = document.getElementById("price");
        let duration = document.getElementById("duration");
        let validation = addServiceValidate();
        if (validation == true) {
            let data = {};

            data['ServiceName'] = servicename.value;
            data['Price'] = Number(price.value);
            data['Duration'] = Number(duration.value);

            let response = await fetch('/adminpaneladdservice',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json;charset=utf-8' },
                    body: JSON.stringify(data)
                });

            if (response.ok) {
                let message = await response.json();
                document.getElementById("lineinfo").innerHTML = message.message  ;
                console.log(message.message);
            }
            else {
                console.log(response.status);
                document.getElementById("lineinfo").innerHTML = message.message ;
            }
        };
    });

    AddWorkhoursButton.addEventListener('click', async () => {
        //document.getElementById("lineinfo").innerHTML = "";
        let mastername = chooseMasterOptionsToAddWorkhours.value;

        let datebegin = new Date(document.getElementById("addworkhoursbegin").value);
        let dateend = new Date(document.getElementById("addworkhoursend").value);
        
        //let validation = addServiceValidate();
        
        let data = {};

        data['MasterName'] = mastername;
        data['WorkHoursBegin'] = datebegin;
        data['WorkHoursEnd'] = dateend;

        console.log(mastername.value);
        let response = await fetch('/adminpaneladdworkhours',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(data)
            });

        if (response.ok) {
            let message = await response.json();
            document.getElementById("lineinfo").innerHTML = message.message;
            console.log(message.message);
        }
        else {
            console.log(response.status);
            document.getElementById("lineinfo").innerHTML = message.message;
        }
        
    });

    addMasterButton.addEventListener('click', async () => {
        document.getElementById("lineinfoaddmaster").innerHTML = "";
        let mastername = document.getElementById("mastername");
        let validation = addMasterValidate();
        if (validation == true) {

            
            let data = {};
            data['Name'] = mastername.value;

            let response = await fetch('/adminpaneladdmaster',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json;charset=utf-8' },
                    body: JSON.stringify(data)
                });

            if (response.ok) {
                let message = await response.json();
                document.getElementById("lineinfoaddmaster").innerHTML = message.message;
                console.log(message.message);
            }
            else {
                console.log(response.status);
                document.getElementById("lineinfoaddmaster").innerHTML = message.message;
            }
        };
    });

    addServiceToMasterButton.addEventListener('click', async () => {
        let lineinfo = document.getElementById("lineinfoaddservicetomaster");
        lineinfo.innerHTML = "";
        let mastername = chooseMasterOptions.value;
        let servicename = chooseServiceOptions.value;
        let data = {};
        data['MasterName'] = mastername;
        data['ServiceName'] = servicename;

        let response = await fetch('/adminpaneladdservicetomaster',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(data)
            });

        if (response.ok) {
            let message = await response.json();
            lineinfo.innerHTML = message.message;
        }
        else {
            console.log(response.status);
            lineinfo.innerHTML = message.message;
        }
        
    });
    DeleteServiceFromMasterButton.addEventListener('click', async () => {
        let lineinfo = document.getElementById("lineinfodeleteservicefrommaster");
        lineinfo.innerHTML = "";
        let mastername = chooseMasterOptionsToDeleteFromMaster.value;
        let servicename = chooseServiceOptionsToDeleteFromMaster.value;
        let data = {};
        data['MasterName'] = mastername;
        data['ServiceName'] = servicename;

        let response = await fetch('/adminpaneldeleteservicefrommaster',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(data)
            });

        if (response.ok) {
            let message = await response.json();
            lineinfo.innerHTML = message.message;
        }
        else {
            console.log(response.status);
            lineinfo.innerHTML = message.message;
        }

    });

    DeleteMasterButton.addEventListener('click', async () => {
        let lineinfodm = document.getElementById("lineinfodeletemaster");
        lineinfodm.innerHTML = "";
        let mastername = chooseMasterOptionsToDelete.value;
        let data = {};
        data['Name'] = mastername;

        let response = await fetch('/adminpaneldeletemaster',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(data)
            });

        if (response.ok) {
            let message = await response.json();
            lineinfodm.innerHTML = message.message;
        }
        else {
            console.log(response.status);
            lineinfodm.innerHTML = message.message;
        }
    });

    DeleteServiceButton.addEventListener('click', async () => {
        let lineinfodm = document.getElementById("lineinfodeleteservice");
        lineinfodm.innerHTML = "";
        let servicename = chooseServiceOptionsToDelete.value;
        let data = {};
        data['ServiceName'] = servicename;
        data['Price'] = 0;
        data['Duration'] = 0;

        let response = await fetch('/adminpaneldeleteservice',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(data)
            });

        if (response.ok) {
            let message = await response.json();
            lineinfodm.innerHTML = message.message;
        }
        else {
            console.log(response.status);
            lineinfodm.innerHTML = message.message;
        }
    });

    ChangePriceButton.addEventListener('click', async () => {
        let lineinfochp = document.getElementById("lineinfochangeprice");
        lineinfochp.innerHTML = "";
        let pricetochange = document.getElementById("pricetochange");
        let servicename = chooseServiceOptionsToChangePrice.value;
        let validation = ChangePriceValidate();
        if (validation == true) {
            let data = {};
            data['ServiceName'] = servicename;
            data['Price'] = Number(pricetochange.value);
            data['Duration'] = 0;

            let response = await fetch('/adminpanelchangeprice',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json;charset=utf-8' },
                    body: JSON.stringify(data)
                });

            if (response.ok) {
                let message = await response.json();
                lineinfochp.innerHTML = message.message;
            }
            else {
                console.log(response.status);
                lineinfochp.innerHTML = message.message;
            }
        }
        
    });

});