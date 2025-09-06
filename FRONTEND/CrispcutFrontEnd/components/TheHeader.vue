<template>
  <header>
    <div class="absolute top-4 left-1/2 -translate-x-1/2 z-10 w-11/12 max-w-md">
      <div class="relative bg-white rounded-full shadow-lg">
        <input
          type="text"
          placeholder="Search for artists or services..."
          class="w-full py-3 pl-12 pr-4 text-gray-700 bg-transparent rounded-full focus:outline-none focus:ring-2 focus:ring-purple-500"
        />
        <div class="absolute inset-y-0 left-0 flex items-center pl-4">
          <svg class="w-5 h-5 text-gray-400" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor">
            <path fill-rule="evenodd" d="M9 3.5a5.5 5.5 0 100 11 5.5 5.5 0 000-11zM2 9a7 7 0 1112.452 4.391l3.328 3.329a.75.75 0 11-1.06 1.06l-3.329-3.328A7 7 0 012 9z" clip-rule="evenodd" />
          </svg>
        </div>
      </div>
    </div>

    <button
      @click="$emit('toggle-sidebar')"
      class="absolute top-4 left-4 z-20 p-2 bg-white rounded-full shadow-lg text-gray-700 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-purple-500"
      aria-label="Open sidebar"
    >
      <svg class="w-6 h-6" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" d="M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25h16.5" />
      </svg>
    </button>
  </header>
</template>

<script setup>
// Define the event this component can emit
defineEmits(['toggle-sidebar']);
</script>