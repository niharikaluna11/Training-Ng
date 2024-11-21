<template>
<ToastifyContainer />
 <router-view/>

</template>

<script>
import { ToastifyContainer } from 'vue3-toastify';

export default {
  name: 'App',
  components: {
    ToastifyContainer
  }
}
</script>

<style>
*{
    
    margin: 0px;
    
    
}
</style>
