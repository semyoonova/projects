// Добавляем обработчики событий для кнопок после загрузки страницы
document.addEventListener('DOMContentLoaded', async() => {
    // Получаем кнопки по их тексту или классам
    const button1 = document.querySelector('button:nth-of-type(1)');
    const button2 = document.querySelector('button:nth-of-type(2)');

    // Назначаем обработчик для первой кнопки
    button1.addEventListener('click', () => {
        window.open(href = "/home", "_self");
    });


    let quotesBlock = document.getElementById("quotes");
    let quoteResponce = await fetch  ("/quotes", {
        method:"GET", 
        headers: {
            'Content-Type': 'application/json;charset=utf-8',
        }
    });
    let quote = await quoteResponce.json();
    quotesBlock.innerHTML= quote.Text;

});