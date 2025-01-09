<template>
  <ToastifyContainer />
  <router-view />
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
:root {
  --c-text-primary: #282a32;
  --c-text-light: #ddd9ee;
  --c-text-secondary: #686b87;
  --c-text-action: #404089;
  --c-accent-primary: #434ce8;
  --c-border-primary: #eff1f6;
  --c-background-primary: #ffffff;
  --c-background-secondary: #fdfcff;
  --c-background-tertiary: #ecf3fe;
  --c-background-quaternary: #e9ecf4;
  --ds-surface-sunken: #F7F8F9;
  --ds-border: #091E4224;
  --new-color: #9484c4;
  --new-bg-col: #f5f5f5;
  --grey: #e7e7e7;
}

body {
  margin-top: 40px;
  background-color: var(--new-bg-col);

}
</style>