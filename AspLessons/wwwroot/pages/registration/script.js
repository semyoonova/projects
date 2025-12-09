charset = "utf-8";
document.addEventListener('DOMContentLoaded', () => {
   
    const button = document.getElementById('register');
    let registerForm = document.getElementById('registerform');
    let name = document.getElementById("name");
    let phone = document.getElementById("phone");
    let password = document.getElementById("password");
    let passwordrepeat = document.getElementById("passwordrepeat");

    function validate() {

        if (!name.validity.valid) {
            document.getElementById("lineinfo").innerHTML += "Имя пользователя не должно быть меньше 2 символов" + '<br />';
            return false;
        }
        if (!phone.validity.valid) {
            document.getElementById("lineinfo").innerHTML += "Телефон должен содержать только цифры" + '<br />';
            document.getElementById("lineinfo").innerHTML += "Телефон должен быть от 7 до 12 символов" + '<br />';
            return false;
        }
        if (!password.validity.valid) {
            document.getElementById("lineinfo").innerHTML += "Пароль должен быть от 8 до 20 символов" + '<br />';
            document.getElementById("lineinfo").innerHTML += "Пароль должен включать как минимум 1 символ в вверхнем ригистре" + '<br />';
            document.getElementById("lineinfo").innerHTML += "Пароль должен включать как минимум 1 специальный символ" + '<br />';
            document.getElementById("lineinfo").innerHTML += "Пароль должен включать как минимум 1 цифру" + '<br />';
            return false;
        }
        if (password.value != passwordrepeat.value) {
            document.getElementById("lineinfo").innerHTML += "Пароли не сопадают" + '<br />';
            return false;
        }
        return true;
    }

    button.addEventListener('click', async () => {
         
        let validation = validate();
        if (validation == true) {
            let data = {};

            data['name'] = name.value;
            data['phoneNumber'] = phone.value;
            data['password'] = password.value;
            data['role'] = "client";


            let response = await fetch(`/user/registration`,
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json;charset=utf-8' },
                    body: JSON.stringify(data)
                });

            if (response.ok) {
                //let message = await response.json();
                //console.log(message);
                //if (message.message == "Пользователь с таким номером уже зарегестрирован") {
                //    document.getElementById("lineinfo").innerHTML = message.message + '<br />';
                //}
                //else {
                    //localStorage.setItem("token", message.Token);
                    window.open(href = "../authorization/index.html", "_self");
                }
            
        }
    });

});