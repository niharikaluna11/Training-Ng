<script>
    export default{
        name:"ProductList",
        data(){
            return{
                products:[],
                btnClick:(args)=>{
                    alert('product added to cart '+args )
                }
            }
        },
        mounted(){
            console.log('component loaded')
            fetch('https://dummyjson.com/products')
                    .then(res => res.json())
                    .then(data=>{
                        console.log(data.products);
                        this.products = data.products
                    });
        }
    }
</script>
<template>
    <section>
        <h1>Products</h1>

        <!-- <button @click="btnClick()" class="btn btn-success">Click me</button> -->
        <div v-if="products.length>0">
            <h2>List</h2>
            <div v-for="product in products" :key="product.id" class="card proddiv">
            <img class="card-img-top" :src=product.thumbnail alt="Card image cap">
            <div class="card-body">
                <h5 class="card-title">{{product.title}}</h5>
                <p class="card-text">{{ product.description }}</p>
                <button @click="btnClick(product.id)" class="btn btn-primary">buy @ {{ product.price }}</button>
            </div>
        </div>
        </div>
    
    </section>
</template>
<style>
.proddiv{
    width:20rem;
    position: relative;
    float:left;
    margin:5px;
}
</style>