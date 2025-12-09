
document.addEventListener('DOMContentLoaded', () => {

    const masterOptions = document.getElementById('choosemaster');
    const button = document.getElementById('next');


    async function getMasters() {
        const headers = { "Content-Type": "application/json;charset=utf-8" };
        const token = localStorage.getItem("token");

        if (token) {
            headers["Authorization"] = `Bearer ${token}`;
        }


        const response = await fetch(`/user/getmasters`, {
            method: "GET",
            headers: headers,
        });

        const data = await response.json(); 

        if (data && Array.isArray(data.masters)) {
            for (let i = 0; i < data.masters.length; i++) {
                let obj = data.masters[i];
                masterOptions.innerHTML += `<option id="${obj.id}" value="${obj.name}">${obj.name}</option>`;
            }
        } else {
            console.error("Некорректный ответ:", data);
        }
    };
    getMasters();

    button.addEventListener('click', async () => {

        const selectedOption = masterOptions.options[masterOptions.selectedIndex];
        const masterId = selectedOption.id;

        localStorage.setItem("masterkey", masterId);
        localStorage.setItem("mastername", selectedOption.value);


        window.open(href = "/pages/services/index.html", "_self");
    });

});
