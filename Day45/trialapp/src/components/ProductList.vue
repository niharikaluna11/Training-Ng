<template>
    <div>
      <h2>Productsss</h2>
      <button @click="btnclick()" class="btn btn-success">clcick me</button>
    </div>
  </template>
  
  <script>
  export default {
    name: 'ProductList',
    data(){
        return {
            btnclick:()=>
        {
            alert("clicked");
        }

        }
    }
    }
  
  </script>
  
  <style >

  </style>
  