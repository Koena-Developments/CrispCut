<template>
  <div class="relative w-screen h-screen overflow-hidden font-sans">
    <TheSidebar :is-open="isSidebarOpen" @close="closeSidebar" />
    <TheHeader @toggle-sidebar="openSidebar" />
    <main class="w-full h-full">
      <ClientOnly>
        <MapDisplay />

        <template #fallback>
          <div class="w-full h-full bg-gray-200 flex items-center justify-center">
            <p class="text-gray-500">Loading map...</p>
          </div>
        </template>
      </ClientOnly>
    </main>
  </div>
</template>

<script setup>
import { ref } from 'vue';

// Reactive state for the sidebar visibility
const isSidebarOpen = ref(false);

const openSidebar = () => {
  isSidebarOpen.value = true;
};

const closeSidebar = () => {
  isSidebarOpen.value = false;
};

// Use the global body style for the font
useHead({
  bodyAttrs: {
    class: 'font-inter'
  },
  htmlAttrs: {
    // Prevent scrollbars on the main page
    class: 'overflow-hidden'
  }
})
</script>

<style>
/* Global styles from the original file. 
  The Nuxt Google Fonts module will apply the font-family.
*/
body, html {
  margin: 0;
  padding: 0;
  width: 100%;
  height: 100%;
}
</style>