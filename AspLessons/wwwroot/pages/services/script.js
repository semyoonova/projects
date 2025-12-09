document.addEventListener('DOMContentLoaded', () => {
    let masterId = localStorage.getItem("masterkey");
    console.log(masterId);

    serviceOptions = document.getElementById('chooseservice');

    
    async function getService() {
        const headers = { "Content-Type": "application/json;charset=utf-8" };
        const token = localStorage.getItem("token");

        if (token) {
            headers["Authorization"] = `Bearer ${token}`;
        } 
        let data = {};
        data['masterId'] = Number(masterId);
        await fetch(`/user/getfavors`, {
            method: "POST",
            headers: headers,
            body: JSON.stringify(data),
        })
        

        .then(response => {
            return response.json();
        })
            .then(data => {
            if (data.message) {
                window.open(href = "/main", "_self");
            }
            else {

                if (data && Array.isArray(data.favors)) {
                    for (let i = 0; i < data.favors.length; i++) {
                        let obj = data.favors[i];
                        serviceOptions.innerHTML += `<option id="${obj.id}" value="${obj.favorName}">${obj.favorName}</option>`;
                    }
                } else {
                    console.error("Некорректный ответ:", data);
                };
            }
            
        });
    }

    getService();
    const button = document.getElementById('next');

    button.addEventListener('click', async () => {
        const selectedOption = serviceOptions.options[serviceOptions.selectedIndex];
        const serviceId = selectedOption.id;

        localStorage.setItem("servicekey", serviceId);
        localStorage.setItem("servicename", selectedOption.value);
        window.open(href = "/pages/datetime/index.html", "_self");
    });
    

});