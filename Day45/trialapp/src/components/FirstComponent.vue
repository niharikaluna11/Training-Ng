<template>
    <div>
      <h2>{{ msg }}</h2>
      <ul class="user-list">
        <li v-for="item in dataList" :key="item.id" class="user-item">
          {{ item.name }}
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    name: 'FirstComponent',
    props: ['msg'],
    data() {
      return {
        dataList: [
          { id: 1, name: 'Johnny Naorem' },
          { id: 2, name: 'Laishram Rohit' },
          { id: 3, name: 'Niharika' },
          { id: 4, name: 'Disha' }
        ]
      };
    }
  };
  </script>
  
  <style scoped>
  .user-list {
    list-style-type: none;
    padding: 0;
    margin-top: 20px;
  }
  
  .user-item {
    padding: 10px;
    margin: 5px 0;
    background-color: #f5f5f5;
    border-radius: 8px;
    box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.2);
    font-size: 18px;
    color: #333;
    text-align: left;
    max-width: 200px;
    margin: auto;
  }
  </style>
  