<template>
  <div>
    <div
      v-if="isOpen"
      @click="$emit('close')"
      class="fixed inset-0 bg-black bg-opacity-50 z-20"
      aria-hidden="true"
    ></div>

    <div
      :class="[
        'fixed top-0 left-0 h-full z-30 transition-transform duration-300 ease-in-out',
        isOpen ? 'translate-x-0' : '-translate-x-full'
      ]"
    >
      <nav class="h-full w-72 bg-white shadow-2xl p-6">
        <div class="flex justify-between items-center mb-8">
          <h2 class="text-2xl font-bold text-gray-800">Trim</h2>
          <button @click="$emit('close')" class="text-gray-500 hover:text-gray-800" aria-label="Close sidebar">
            <svg class="w-6 h-6" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>
        </div>
        <ul class="space-y-4">
          <li><a href="#" class="flex items-center p-2 text-lg text-gray-700 rounded-lg hover:bg-gray-100"><span class="ml-3">Sign In / Sign Up</span></a></li>
          <li><a href="#" class="flex items-center p-2 text-lg text-gray-700 rounded-lg hover:bg-gray-100"><span class="ml-3">Register as an Artist</span></a></li>
          <li><a href="#" class="flex items-center p-2 text-lg text-gray-700 rounded-lg hover:bg-gray-100"><span class="ml-3">About</span></a></li>
          <li><a href="#" class="flex items-center p-2 text-lg text-gray-700 rounded-lg hover:bg-gray-100"><span class="ml-3">Help</span></a></li>
        </ul>
      </nav>
    </div>
  </div>
</template>

<script setup>
// Define the props and emits for this component
defineProps({
  isOpen: {
    type: Boolean,
    required: true
  }
});
defineEmits(['close']);
</script>