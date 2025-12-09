document.addEventListener('DOMContentLoaded', () => {

    const button = document.getElementById('authorization');
    function validate() {

        
        if (!phone.validity.valid) {
            document.getElementById("lineinfo").innerHTML += "Телефон должен содержать только цифры" + '<br />';
            document.getElementById("lineinfo").innerHTML += "Телефон должен быть от 7 до 12 символов" + '<br />';
            return false;
        }
        return true;
    }
    button.addEventListener('click', async () => {

        let validation = validate();
        if (validation == true) {
            let phonevalue = document.getElementById("phone").value;
            let passwordvalue = document.getElementById("password").value;

            let data = {};
            data['phoneNumber'] = phonevalue;
            data['password'] = passwordvalue;

            let response = await fetch('/user/auth',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json;charset=utf-8' },
                    body: JSON.stringify(data)
                });

            if (response.ok) {
                const responseData = await response.json();
                localStorage.setItem('token', responseData);
                window.open(href = "../masters/index.html", "_self");
                } 
            }
            else {
                console.log(response.status);
            }

        


    });

});