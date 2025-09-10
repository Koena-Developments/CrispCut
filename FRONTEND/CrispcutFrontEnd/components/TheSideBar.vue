<template>
  <div>
    <!-- Sidebar for Artists (fixed, always visible on large screens) -->
    <template v-if="userStore.isArtist">
      <aside class="fixed top-0 left-0 h-full w-20 bg-gray-800 text-white flex flex-col items-center py-4 shadow-xl z-50">
        <div class="mb-8">
          <NuxtLink to="/">
            <img src="/logo-icon.svg" alt="Logo" class="w-10 h-10 transition-transform duration-300 transform hover:scale-110">
          </NuxtLink>
        </div>
        
        <nav class="flex-1 flex flex-col items-center space-y-4">
          <NuxtLink to="/artistpages" class="p-3 rounded-xl hover:bg-gray-700 transition-colors duration-200">
            <svg class="h-6 w-6 text-gray-400 group-hover:text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m-6 0h6"></path>
            </svg>
          </NuxtLink>
          <span class="text-xs text-gray-500 mt-1">Dashboard</span>
          
          <NuxtLink to="/artistpages" class="p-3 rounded-xl hover:bg-gray-700 transition-colors duration-200">
            <svg class="h-6 w-6 text-gray-400 group-hover:text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 14v3m4-3v3m4-3v3M18 10h-2.115a2 2 0 00-1.789 1.106L15 14h-5l-.106-.894A2 2 0 007.115 10H5m4 4h6m-6 0H9m-6-4h.01M16 10h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
            </svg>
          </NuxtLink>
          <span class="text-xs text-gray-500 mt-1">Services</span>
    
          <NuxtLink to="/artistpages" class="p-3 rounded-xl hover:bg-gray-700 transition-colors duration-200">
            <svg class="h-6 w-6 text-gray-400 group-hover:text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z"></path>
            </svg>
          </NuxtLink>
          <span class="text-xs text-gray-500 mt-1">Profile</span>
        </nav>
      </aside>
    </template>

    <!-- Sidebar for a normal user (mobile-only, slides in from the right) -->
    <template v-else>
      <div 
        class="fixed inset-0 z-40 bg-black/50 backdrop-blur-sm transition-opacity duration-300 md:hidden"
        :class="{ 'opacity-100': isOpen, 'opacity-0 pointer-events-none': !isOpen }"
        @click="$emit('close')"
      ></div>

      <div 
        class="fixed top-0 right-0 h-full w-64 bg-slate-900 border-l border-gray-700 z-50 transform transition-transform duration-300 ease-in-out"
        :class="{ 'translate-x-0': isOpen, 'translate-x-full': !isOpen }"
      >
        <div class="flex flex-col h-full p-6 space-y-8">
          <div class="flex justify-end items-center mb-6">
            <button @click="$emit('close')" class="text-white hover:text-blue-500 transition-colors duration-200">
              <svg class="w-8 h-8" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
              </svg>
            </button>
          </div>
          
          <template v-if="isLoggedIn">
            <div class="flex flex-col items-center mb-8">
              <div class="relative w-24 h-24 rounded-full overflow-hidden mb-3 border-4 border-blue-600 shadow-lg">
                <img 
                  :src="user.profilePictureUrl" 
                  alt="User Avatar" 
                  class="w-full h-full object-cover" 
                />
                <div class="absolute bottom-1 right-1 w-4 h-4 rounded-full bg-green-500 border-2 border-gray-800 animate-pulse"></div>
              </div>
              <h2 class="text-xl font-bold text-white">{{ user.firstName }}</h2>
              <p class="text-sm text-green-500 font-semibold mt-1">Logged In</p>
            </div>

            <nav class="flex-grow">
              <ul class="space-y-4">
                <li>
                  <a href="#" class="flex items-center space-x-4 p-3 rounded-lg text-gray-400 hover:bg-blue-600 hover:text-white transition-colors duration-200">
                    <svg class="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.527.245 1.05.342 1.571.397M12 8.5a3.5 3.5 0 110 7 3.5 3.5 0 010-7z"></path></svg>
                    <span class="font-medium">Settings</span>
                  </a>
                </li>
                <li>
                  <a href="#" class="flex items-center space-x-4 p-3 rounded-lg text-gray-400 hover:bg-blue-600 hover:text-white transition-colors duration-200">
                    <svg class="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 11.5V14m0-2.5v2.5m0-2.5h2.5m-2.5 0H7m0 2.5h2.5m-2.5 0H7M12 4v16M12 4v16a8 8 0 00-8 8m8-8a8 8 0 008-8"></path></svg>
                    <span class="font-medium">Promos</span>
                  </a>
                </li>
                <li>
                  <a href="#" class="flex items-center space-x-4 p-3 rounded-lg text-gray-400 hover:bg-blue-600 hover:text-white transition-colors duration-200">
                    <svg class="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c1.657 0 3 1.343 3 3v2a3 3 0 01-3 3 3 3 0 01-3-3v-2c0-1.657 1.343-3 3-3z"></path><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 13v2a5 5 0 0010 0v-2"></path><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 18v2m-5-8h10M7 6h10"></path></svg>
                    <span class="font-medium">Conflicts</span>
                  </a>
                </li>
                <li>
                  <a href="#" class="flex items-center space-x-4 p-3 rounded-lg text-gray-400 hover:bg-blue-600 hover:text-white transition-colors duration-200">
                    <svg class="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h.01M16 11h.01M9 19h.01M15 19h.01M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z"></path></svg>
                    <span class="font-medium">Booking History</span>
                  </a>
                </li>
              </ul>
            </nav>
            
            <button
              @click="handleLogout"
              class="w-full text-left py-3 px-4 rounded-xl font-semibold text-lg bg-red-600 text-white shadow-lg hover:bg-red-700 transition-colors"
            >
              Logout
            </button>
          </template>

          <template v-else>
            <button
              @click="goToAuth"
              class="w-full text-left py-3 px-4 rounded-xl font-semibold text-lg bg-blue-600 text-white shadow-lg hover:bg-blue-700 transition-colors"
            >
              Login / Sign Up
            </button>
          </template>
        </div>
      </div>
    </template>
  </div>
</template>

<script setup>
import { defineProps, defineEmits } from 'vue';
import { navigateTo } from '#app';
import { useUserStore } from '@/stores/UserStore';
import { storeToRefs } from 'pinia'; 

const userStore = useUserStore();
const { isLoggedIn, user, isArtist } = storeToRefs(userStore); 

const props = defineProps({
  isOpen: {
    type: Boolean,
    required: true,
  },
});

const emit = defineEmits(['close']);

const goToAuth = () => {
  navigateTo('/auth');
  emit('close');
};

const handleLogout = () => {
  userStore.logout();
  navigateTo('/auth');
  emit('close');
};
</script>

<style scoped>
.h-full {
  min-height: 100vh;
}
</style>
