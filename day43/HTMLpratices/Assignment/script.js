


const clickEvent = () => {
    fetch('https://dummyjson.com/products')
        .then(response => response.json())
        .then(json => {
            const products = json.products;
            const cardsContainer = document.querySelector('.cards');

            // Clear existing content
            cardsContainer.innerHTML = '';

            // Create a card for each product
            products.forEach(product => {
                const card = document.createElement('article');
                card.classList.add('card');

                const figure = document.createElement('figure');

                const img = document.createElement('img');
                img.src = product.thumbnail;
                img.alt = product.title;

                const middle = document.createElement('div');
                middle.classList.add('middle');
                const text1 = document.createElement('div');
                text1.classList.add('text1');
                const text2 = document.createElement('div');
                text2.classList.add('text2');
                const buttonnew = document.createElement('button');
                buttonnew.classList.add('button-14');

                text1.textContent = product.description;
                text2.textContent = `Price: ${product.price} Rs`;
                buttonnew.textContent = "Buy";
                buttonnew.onclick = () => BuyProduct(product.id); // Passing the product object to BuyProduct function

                const caption = document.createElement('figcaption');
                caption.textContent = product.title;

                card.appendChild(figure);
                cardsContainer.appendChild(card);
                figure.appendChild(img);
                figure.appendChild(middle);
                middle.appendChild(text1);
                middle.appendChild(text2);
                middle.appendChild(buttonnew);
                figure.appendChild(caption);
            });
        })
        .catch(error => console.error('Error fetching data:', error));
};


let productToBeAdd;

const BuyProduct = (productId) => {
    const cardsContainer = document.querySelector('.cards');
    
    // Prompt the user to enter the quantity
    const quantity = prompt("Enter the quantity of the product:");

    // Ensure the input is a valid number and greater than zero
    if (isNaN(quantity) || quantity <= 0) {
        alert("Please enter a valid quantity.");
        return;
    }

    fetch('https://dummyjson.com/carts/add', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            userId: 1, 
            products: [
                { id: productId, quantity: parseInt(quantity) } // Use the user-entered quantity
            ]
        })
    })
    .then((res) => res.json()) 
    .then(json => {
        productToBeAdd = json; 
        cardsContainer.innerHTML = ''; 
        
        GetResponse(productToBeAdd);
    })
    .catch(error => console.error('Error adding product to cart:', error));
};


const GetResponse = (productToBeAdd) => {
    const cardsContainer = document.querySelector('.centered');
    cardsContainer.innerHTML = ''; 


    const responseDiv1 = document.createElement('div');
    responseDiv1.classList.add('text'); 

    const responseText1 = document.createElement('pre'); 
    responseText1.textContent = "Thankyou For your purchase. ";

    const responseDiv = document.createElement('div');
    responseDiv.classList.add('response'); 

    const responseText = document.createElement('pre'); 
    responseText.textContent = JSON.stringify(productToBeAdd, null, 2); 

    responseDiv1.appendChild(responseText1);
    responseDiv.appendChild(responseText);
    cardsContainer.appendChild(responseDiv1);
    cardsContainer.appendChild(responseDiv);

    console.log(productToBeAdd);
};
