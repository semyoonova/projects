document.addEventListener('DOMContentLoaded', () => {

    async function getDates() {
        const headers = { "Content-Type": "application/json" };
        const token = localStorage.getItem("token");

        if (token) {
            headers["Authorization"] = `Bearer ${token}`;
        } 
        let data = {};
        data['masterId'] = localStorage.getItem("masterkey");
        await fetch(`/user/getdates`, {
            method: "POST",
            headers: headers,
            body: JSON.stringify(data)
        })
            .then(response => {
                return response.json();
            })
            .then(data => {
                if (data.message) {
                    window.open(href = "/main", "_self");
                }
                else {
                    for (let i = 0; i < data.dates.length; i++) {
                        let obj = data.dates[i];
                        
                        chooseDateOptions.innerHTML += `<option  value="${obj}">${obj}</option>`;

                    };
                }
            });
        getSlots();
    }

    async function getSlots() {
        const dateObj = chooseDateOptions.value;
        console.log(dateObj);
        const dateString = dateObj;
        let data = {};
        data['date'] = dateString;
        data['masterId'] = localStorage.getItem("masterkey");
        data['favorId'] = localStorage.getItem("servicekey");

        console.log(data);
        const headers = { "Content-Type": "application/json;charset=utf-8" };
        const token = localStorage.getItem("token");

        if (token) {
            headers["Authorization"] = `Bearer ${token}`;
        }
        let response = await fetch(`/user/getslots`,
            {
                method: 'POST',
                headers: headers,
                body: JSON.stringify(data)
            })
            .then(response => {
                return response.json();
            })
            .then(data => {
                console.log(data);
                if (data.message) {
                    window.open(href = "/main", "_self");
                }
                else {
                    for (let i = 0; i < data.slots.length; i++) {
                        let obj = data.slots[i];
                        chooseTimeOptions.innerHTML += "<option value=" + obj + ">" + obj + "</option>";
                    };
                }
            });
    }


    const button = document.getElementById('next');
    const chooseDateOptions = document.getElementById('chooseday');
    const chooseTimeOptions = document.getElementById('choosetime');
    getDates();
    
    chooseDateOptions.addEventListener('change', async () => {
        getSlots();
    });

    
    

    button.addEventListener('click', async () => {
        localStorage.setItem('datekey', chooseDateOptions.value);
        localStorage.setItem('timekey', chooseTimeOptions.value);
        date = localStorage.getItem("datekey");
        time = localStorage.getItem("timekey");
        const dateTimeStr = `${date}T${time}`;
        let dateD = new Date(dateTimeStr).toISOString();

        let data = {};
        data['masterId'] = localStorage.getItem("masterkey");
        data['favorId'] = localStorage.getItem("servicekey");
        data['date'] = chooseDateOptions.value;
        data['time'] = chooseTimeOptions.value;
        const token = localStorage.getItem("token");
        const headers = { "Content-Type": "application/json;charset=utf-8" };
        if (token) {
            headers["Authorization"] = `Bearer ${token}`;
        }
        let response = await fetch(`/user/register`,
            {
                method: 'POST',
                headers: headers,
                body: JSON.stringify(data)
            });
        console.log(response);
        if (response.ok) {
            console.log("Все супер");
            window.open(href = "../success/index.html", "_self");
        }
        else {
            console.log(response.status);
        }
        });
});


    
 

    
