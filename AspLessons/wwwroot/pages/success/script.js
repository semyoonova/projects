document.addEventListener('DOMContentLoaded', () => {

    const text = document.getElementById("success");
    text.innerHTML = "Вы записаны" + '<br >' + "на услугу " + localStorage.getItem("servicename") + '<br >'
        + "к мастеру " + localStorage.getItem("mastername") + '<br >'
        + " на " + localStorage.getItem("datekey") +
        " в " + localStorage.getItem("timekey");
    
    const button = document.getElementById('next');

    button.addEventListener('click', () => {
        window.open(href = "/pages/masters/index.html", "_self");
        localStorage.removeItem("masterkey");
        localStorage.removeItem("datekey");
        localStorage.removeItem("timekey");
        localStorage.removeItem("servicekey");
        localStorage.removeItem("mastername");
        localStorage.removeItem("servicename");
    });
});