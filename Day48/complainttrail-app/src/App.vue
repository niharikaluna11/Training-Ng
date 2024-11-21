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
@import url("https://fonts.googleapis.com/css2?family=Be+Vietnam+Pro:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap");

:root {
  --c-text-primary: #282a32;
  --c-text-secondary: #686b87;
  --c-text-action: #404089;
  --c-accent-primary: #434ce8;
  --c-border-primary: #eff1f6;
  --c-background-primary: #ffffff;
  --c-background-secondary: #fdfcff;
  --c-background-tertiary: #ecf3fe;
  --c-background-quaternary: #e9ecf4;
}

body {
  line-height: 1.5;
  min-height: 100vh;
  font-family: "Be Vietnam Pro", sans-serif;
  background-color: var(--c-background-secondary);
  color: var(--c-text-primary);
}


:focus {
  outline: 0;
}

.responsive-wrapper {
  width: 80%;
  max-width: 1280px;
  margin-left: auto;
  margin-right: auto;
}

.header {
  display: flex;
  align-items: center;
  height: 80px;
  border-bottom: 1px solid var(--c-border-primary);
  background-color: var(--c-background-primary);
}

.content-header {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-between;
  padding-top: 3rem;
  margin-top: -1px;
}

.content-header-intro {
  h2 {
    font-size: 1.25rem;
    font-weight: 600;
  }

  p {
    color: var(--c-text-secondary);
    margin-top: 0.25rem;
    font-size: 0.875rem;
    margin-bottom: 1rem;
  }
}

.content-header-actions {
  a:first-child {
    @media (min-width: 500px) {
      display: none;
    }
  }
}

.content {
  border-top: 1px solid var(--c-border-primary);
  margin-top: 2rem;
  display: flex;
  align-items: flex-start;
}

.header-content {
  display: flex;
  align-items: center;

  &>a {
    display: none;
  }

  @media (max-width: 1200px) {
    justify-content: space-between;

    &>a {
      display: inline-flex;
    }
  }
}

.header-logo {
  margin-right: 2.5rem;

  a {
    display: flex;
    align-items: center;

    div {

      flex-shrink: 0;
      position: relative;

      &:after {
        display: block;
        content: "";
        position: absolute;
        left: 0;
        top: auto;
        right: 0;
        bottom: 0;
        overflow: hidden;
        height: 50%;
        border-bottom-left-radius: 8px;
        border-bottom-right-radius: 8px;
        background-color: rgba(#fff, 0.2);
        backdrop-filter: blur(4px);
      }
    }
  }
}

.header-navigation {
  display: flex;
  flex-grow: 1;
  align-items: center;
  justify-content: space-between;

  @media (max-width: 1200px) {
    display: none;
  }
}

.header-navigation-links {
  display: flex;
  align-items: center;

  a {
    text-decoration: none;
    color: var(--c-text-action);
    font-weight: 500;
    transition: 0.15s ease;

    &+* {
      margin-left: 1.5rem;
    }

    &:hover,
    &:focus {
      color: var(--c-accent-primary);
    }
  }
}

.header-navigation-actions {
  display: flex;
  align-items: center;

  &>.avatar {
    margin-left: 0.75rem;
  }

  &>.icon-button+.icon-button {
    margin-left: 0.25rem;
  }

  &>.button+.icon-button {
    margin-left: 1rem;
  }
}

.button {
  font: inherit;
  color: inherit;
  text-decoration: none;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  padding: 0 1em;
  height: 40px;
  border-radius: 8px;
  line-height: 1;
  border: 2px solid var(--c-border-primary);
  color: var(--c-text-action);
  font-size: 0.875rem;
  transition: 0.15s ease;
  background-color: var(--c-background-primary);

  i {
    margin-right: 0.5rem;
    font-size: 1.25em;
  }

  span {
    font-weight: 500;
  }

  &:hover,
  &:focus {
    border-color: var(--c-accent-primary);
    color: var(--c-accent-primary);
  }
}

.icon-button {
  font: inherit;
  color: inherit;
  text-decoration: none;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: 40px;
  height: 40px;
  border-radius: 8px;
  color: var(--c-text-action);
  transition: 0.15s ease;

  i {
    font-size: 1.25em;
  }

  &:focus,
  &:hover {
    background-color: var(--c-background-tertiary);
    color: var(--c-accent-primary);
  }
}

.avatar {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: 44px;
  height: 44px;
  border-radius: 50%;
  overflow: hidden;
}

.main {
  padding-top: 3rem;
}

.main-header {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-between;

  h1 {
    font-size: 1.75rem;
    font-weight: 600;
    line-height: 1.25;

    @media (max-width: 550px) {
      margin-bottom: 1rem;
    }
  }
}

.search {
  position: relative;
  display: flex;
  align-items: center;
  width: 100%;
  max-width: 340px;

  input {
    font: inherit;
    color: inherit;
    text-decoration: none;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    padding: 0 1em 0 36px;
    height: 40px;
    border-radius: 8px;
    border: 2px solid var(--c-border-primary);
    color: var(--c-text-action);
    font-size: 0.875rem;
    transition: 0.15s ease;
    width: 100%;
    line-height: 1;

    &::placeholder {
      color: var(--c-text-action);
    }

    &:focus,
    &:hover {
      border-color: var(--c-accent-primary);
    }
  }

  button {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    border: 0;
    background-color: transparent;
    position: absolute;
    left: 12px;
    top: 50%;
    transform: translateY(-50%);
    font-size: 1.25em;
    color: var(--c-text-action);
    padding: 0;
    height: 40px;
  }
}





html {
  &::-webkit-scrollbar {
    width: 12px;
  }

  &::-webkit-scrollbar-thumb {
    background-color: var(--c-text-primary);
    border: 4px solid var(--c-background-primary);
    border-radius: 99em;
  }
}
</style>
