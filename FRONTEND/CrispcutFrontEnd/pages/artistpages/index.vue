<template>
  <div class="p-8 bg-gray-900 text-white min-h-screen">
    <h1 class="text-4xl font-extrabold text-blue-400 mb-2">Artist Dashboard</h1>
    <p class="text-lg text-gray-400 mb-10">Your performance insights at a glance.</p>
    <TheSidebar :is-open="isSidebarOpen" @close="closeSidebar" />
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
      <!-- Total Bookings Card -->
      <div class="bg-gray-800 p-6 rounded-2xl shadow-xl transition-all hover:scale-[1.02]">
        <h2 class="text-2xl font-bold mb-4 flex items-center">
          <svg class="w-7 h-7 mr-2 text-blue-400" fill="none" stroke="currentColor" viewBox="0 0 24 24"
            xmlns="http://www.w3.org/2000/svg">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
              d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"></path>
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
              d="M15 11a3 3 0 11-6 0 3 3 0 016 0z"></path>
          </svg>
          Total Bookings
        </h2>
        <p class="text-4xl font-extrabold text-white">42</p>
        <p class="text-gray-400 mt-2">Bookings this month</p>
      </div>

      <!-- Repeat Users Card -->
      <div class="bg-gray-800 p-6 rounded-2xl shadow-xl transition-all hover:scale-[1.02]">
        <h2 class="text-2xl font-bold mb-4 flex items-center">
          <svg class="w-7 h-7 mr-2 text-green-400" fill="none" stroke="currentColor" viewBox="0 0 24 24"
            xmlns="http://www.w3.org/2000/svg">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
              d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a4 4 0 014-4h12a4 4 0 014 4v1a4 4 0 01-4 4H15a4 4 0 01-4-4z"></path>
          </svg>
          Returning Clients
        </h2>
        <p class="text-4xl font-extrabold text-white">18</p>
        <p class="text-gray-400 mt-2">Users who booked more than once</p>
      </div>

      <!-- Top Services Card -->
      <div class="bg-gray-800 p-6 rounded-2xl shadow-xl transition-all hover:scale-[1.02]">
        <h2 class="text-2xl font-bold mb-4 flex items-center">
          <svg class="w-7 h-7 mr-2 text-yellow-400" fill="none" stroke="currentColor" viewBox="0 0 24 24"
            xmlns="http://www.w3.org/2000/svg">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
              d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m-6 0h4m2 0a2 2 0 002 2h2a2 2 0 002-2zm0-16a2 2 0 012-2h2a2 2 0 012 2v10a2 2 0 01-2 2h-2a2 2 0 01-2-2V9z"></path>
          </svg>
          Top Services
        </h2>
        <ul class="text-lg space-y-2 mt-4 text-gray-300">
          <li class="flex justify-between items-center">
            <span>Trim</span>
            <span class="font-bold text-white">30%</span>
          </li>
          <li class="flex justify-between items-center">
            <span>Cut and Dye</span>
            <span class="font-bold text-white">25%</span>
          </li>
          <li class="flex justify-between items-center">
            <span>Brush</span>
            <span class="font-bold text-white">20%</span>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import TheSidebar from '~/components/TheSidebar.vue';

const isSidebarOpen = ref(false);

const openSidebar = () => {
  isSidebarOpen.value = true;
};

const closeSidebar = () => {
  isSidebarOpen.value = false;
};

// Mock data for the dashboard charts
// You would replace this with data fetched from your backend API
const mockData = ref({
  monthlyBookings: {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    data: [12, 19, 3, 5, 2, 3],
  },
  repeatClients: {
    labels: ['New Clients', 'Returning Clients'],
    data: [24, 18],
  },
  serviceBreakdown: {
    labels: ['Brush', 'Cut and Dye', 'Cut and Dye with Heart', 'Trim'],
    data: [30, 25, 20, 15],
  },
});
</script>
