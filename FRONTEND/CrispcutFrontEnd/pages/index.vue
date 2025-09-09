<template>
  <div class="relative w-screen h-screen overflow-hidden font-sans">
    <!-- The Sidebar component will now use the Pinia store to get user data -->
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

const isSidebarOpen = ref(false);

const openSidebar = () => {
  isSidebarOpen.value = true;
};

const closeSidebar = () => {
  isSidebarOpen.value = false;
};

// This tells Nuxt to use the 'auth' middleware before loading this page
definePageMeta({
  middleware: 'authentication'
});

useHead({
  bodyAttrs: {
    class: 'font-inter'
  },
  htmlAttrs: {
    class: 'overflow-hidden'
  }
})
</script>

<style>
body, html {
  margin: 0;
  padding: 0;
  width: 100%;
  height: 100%;
}
</style>
