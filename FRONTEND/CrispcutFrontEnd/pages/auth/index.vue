<template>
  <div class="min-h-screen flex items-center justify-center p-4 bg-slate-900">
    <!-- Main Card Container -->
    <div class="bg-gray-800 p-8 rounded-2xl shadow-2xl w-full max-w-lg border border-gray-700">
      <div class="flex flex-col items-center mb-6">
        <h1 class="text-4xl font-extrabold text-white">
          {{ authMode === 'login' ? 'Welcome Back!' : 'Join Us Today!' }}
        </h1>
        <p class="mt-2 text-gray-400 text-center">
          {{ authMode === 'login' ? 'Sign in to access your account.' : 'Create your account to get started.' }}
        </p>
      </div>

      <!-- Toggle between Login and Signup/Artist -->
      <div class="flex justify-center mb-6 w-full">
        <button
          @click="authMode = 'login'"
          :class="{ 'bg-blue-600 text-white shadow-lg': authMode === 'login', 'bg-gray-700 text-gray-400': authMode !== 'login' }"
          class="px-4 py-2 rounded-l-full font-semibold transition-all duration-300 transform hover:scale-105 flex-1"
        >
          Login
        </button>
        <button
          @click="authMode = 'register'"
          :class="{ 'bg-blue-600 text-white shadow-lg': authMode === 'register', 'bg-gray-700 text-gray-400': authMode !== 'register' }"
          class="px-4 py-2 font-semibold transition-all duration-300 transform hover:scale-105 flex-1"
        >
          Sign Up
        </button>
        <button
          @click="authMode = 'artist'"
          :class="{ 'bg-blue-600 text-white shadow-lg': authMode === 'artist', 'bg-gray-700 text-gray-400': authMode !== 'artist' }"
          class="px-4 py-2 rounded-r-full font-semibold transition-all duration-300 transform hover:scale-105 flex-1"
        >
          Artist
        </button>
      </div>

      <!-- Login Form -->
      <form v-if="authMode === 'login'" @submit.prevent="handleAuth" class="space-y-6">
        <div>
          <label for="email" class="block text-sm font-medium text-gray-300">Email</label>
          <input
            id="email"
            v-model="authData.email"
            type="email"
            required
            autocomplete="email"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="your-email@example.com"
          >
        </div>
        <div>
          <label for="password" class="block text-sm font-medium text-gray-300">Password</label>
          <input
            id="password"
            v-model="authData.password"
            type="password"
            required
            autocomplete="current-password"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="••••••••"
          >
        </div>
        <button
          type="submit"
          :disabled="isLoading"
          class="w-full py-3 px-4 bg-blue-600 text-white font-bold rounded-lg shadow-md hover:bg-blue-700 transition-all duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 focus:ring-offset-gray-800 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <span v-if="isLoading" class="flex items-center justify-center">
            <svg class="animate-spin h-5 w-5 mr-3 text-white" viewBox="0 0 24 24">
              <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
              <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
            </svg>
            Logging In...
          </span>
          <span v-else>Login</span>
        </button>
      </form>

      <!-- Standard User Signup Form -->
      <form v-else-if="authMode === 'register'" @submit.prevent="handleAuth" class="space-y-4">
        <div>
          <label for="firstName" class="block text-sm font-medium text-gray-300">First Name</label>
          <input
            id="firstName"
            v-model="authData.firstName"
            type="text"
            required
            autocomplete="given-name"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="John"
          >
        </div>
        <div>
          <label for="lastName" class="block text-sm font-medium text-gray-300">Last Name</label>
          <input
            id="lastName"
            v-model="authData.lastName"
            type="text"
            required
            autocomplete="family-name"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="Doe"
          >
        </div>
        <div>
          <label for="email-signup" class="block text-sm font-medium text-gray-300">Email</label>
          <input
            id="email-signup"
            v-model="authData.email"
            type="email"
            required
            autocomplete="email"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="your-email@example.com"
          >
        </div>
        <div>
          <label for="phone-signup" class="block text-sm font-medium text-gray-300">Phone Number</label>
          <input
            id="phone-signup"
            v-model="authData.phoneNumber"
            type="tel"
            required
            autocomplete="tel"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="+1 555-555-5555"
          >
        </div>
        <div>
          <label for="password-signup" class="block text-sm font-medium text-gray-300">Password</label>
          <input
            id="password-signup"
            v-model="authData.password"
            type="password"
            required
            autocomplete="new-password"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="••••••••"
          >
        </div>
        <button
          type="submit"
          :disabled="isLoading"
          class="w-full py-3 px-4 bg-blue-600 text-white font-bold rounded-lg shadow-md hover:bg-blue-700 transition-all duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 focus:ring-offset-gray-800 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <span v-if="isLoading" class="flex items-center justify-center">
            <svg class="animate-spin h-5 w-5 mr-3 text-white" viewBox="0 0 24 24">
              <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
              <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
            </svg>
            Signing Up...
          </span>
          <span v-else>Sign Up</span>
        </button>
      </form>
      
      <!-- Artist Signup Form -->
      <form v-else @submit.prevent="handleAuth" class="space-y-4">
        <div>
          <label for="firstName" class="block text-sm font-medium text-gray-300">First Name</label>
          <input
            id="firstName"
            v-model="authData.firstName"
            type="text"
            required
            autocomplete="given-name"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="John"
          >
        </div>
        <div>
          <label for="lastName" class="block text-sm font-medium text-gray-300">Last Name</label>
          <input
            id="lastName"
            v-model="authData.lastName"
            type="text"
            required
            autocomplete="family-name"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="Doe"
          >
        </div>
        <div>
          <label for="email-artist" class="block text-sm font-medium text-gray-300">Email</label>
          <input
            id="email-artist"
            v-model="authData.email"
            type="email"
            required
            autocomplete="email"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="your-email@example.com"
          >
        </div>
        <div>
          <label for="phone-artist" class="block text-sm font-medium text-gray-300">Phone Number</label>
          <input
            id="phone-artist"
            v-model="authData.phoneNumber"
            type="tel"
            required
            autocomplete="tel"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="+1 555-555-5555"
          >
        </div>
        <div>
          <label for="password-artist" class="block text-sm font-medium text-gray-300">Password</label>
          <input
            id="password-artist"
            v-model="authData.password"
            type="password"
            required
            autocomplete="new-password"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="••••••••"
          >
        </div>

        <!-- Artist Specific Fields -->
        <div>
          <label for="bio" class="block text-sm font-medium text-gray-300">Bio</label>
          <textarea
            id="bio"
            v-model="authData.bio"
            required
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="Tell us about yourself and your art."
          ></textarea>
        </div>
        <div>
          <label for="category" class="block text-sm font-medium text-gray-300">Category</label>
          <select
            id="category"
            v-model="authData.category"
            required
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
          >
            <option value="" disabled>Select a category</option>
            <option value="Barber">Barber</option>
            <option value="MakeupArtist">MakeupArtist</option>
            <option value="TattooArtist">TatooArtist</option>
            <option value="Hairstylist">Hairstylist</option>
            <!-- <option value="Other">Other</option> -->
          </select>
        </div>
        <div>
          <label for="address" class="block text-sm font-medium text-gray-300">Address</label>
          <input
            id="address"
            v-model="authData.address"
            type="text"
            required
            autocomplete="street-address"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="123 Art Street, City, Country"
          >
        </div>
        <div class="flex space-x-4">
          <div class="flex-1">
            <label for="locationLat" class="block text-sm font-medium text-gray-300">Latitude</label>
            <input
              id="locationLat"
              v-model.number="authData.locationLat"
              type="number"
              required
              step="0.000001"
              class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
              placeholder="e.g., 34.052235"
            >
          </div>
          <div class="flex-1">
            <label for="locationLng" class="block text-sm font-medium text-gray-300">Longitude</label>
            <input
              id="locationLng"
              v-model.number="authData.locationLng"
              type="number"
              required
              step="0.000001"
              class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
              placeholder="e.g., -118.243683"
            >
          </div>
        </div>
        <div>
          <label for="operatingHours" class="block text-sm font-medium text-gray-300">Operating Hours (Optional)</label>
          <input
            id="operatingHours"
            v-model="authData.operatingHours"
            type="text"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="e.g., Mon-Fri, 9:00 AM - 5:00 PM"
          >
        </div>
        <div>
          <label for="certificate" class="block text-sm font-medium text-gray-300">Certificate URL (Optional)</label>
          <input
            id="certificate"
            v-model="authData.certificate"
            type="url"
            class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            placeholder="e.g., https://my-certificate.com/image.jpg"
          >
        </div>

        <button
          type="submit"
          :disabled="isLoading"
          class="w-full py-3 px-4 bg-blue-600 text-white font-bold rounded-lg shadow-md hover:bg-blue-700 transition-all duration-300 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 focus:ring-offset-gray-800 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <span v-if="isLoading" class="flex items-center justify-center">
            <svg class="animate-spin h-5 w-5 mr-3 text-white" viewBox="0 0 24 24">
              <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
              <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
            </svg>
            Signing Up...
          </span>
          <span v-else>
            {{ authMode === 'artist' ? 'Register as Artist' : 'Sign Up' }}
          </span>
        </button>
      </form>
      
      <!-- Error & Success Messages -->
      <p v-if="authError" class="mt-4 text-center text-red-400 font-medium">{{ authError }}</p>
      <p v-if="authSuccess" class="mt-4 text-center text-green-400 font-medium">{{ authSuccess }}</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useUserStore } from '@/stores/UserStore'; 

const authMode = ref('login'); // Can be 'login', 'register', or 'artist'
const authData = ref({
  email: '',
  password: '',
  firstName: '',
  lastName: '',
  phoneNumber: '',
  bio: '',
  category: '',
  address: '',
  locationLat: null,
  locationLng: null,
  operatingHours: '',
  certificate: '',
});

const isLoading = ref(false);
const authError = ref('');
const authSuccess = ref('');

const router = useRouter();
const userStore = useUserStore();

onMounted(() => {
  if (userStore.isLoggedIn) {
    router.push('/');
  }
});

const handleAuth = async () => {
  isLoading.value = true;
  authError.value = '';
  authSuccess.value = '';

  let endpoint = '';
  const payload = authData.value;

  if (authMode.value === 'login') {
    endpoint = 'http://localhost:5007/api/Auth/login';
  } else if (authMode.value === 'register') {
    endpoint = 'http://localhost:5007/api/Auth/register';
  } else if (authMode.value === 'artist') {
    endpoint = 'http://localhost:5007/api/Artists/onboard';
  }
  
  // Log the payload being sent to the API
  console.log('Auth data being sent:', payload);

  try {
    const response = await fetch(endpoint, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(payload)
    });

    if (response.ok) {
      const data = await response.json();
      authSuccess.value = data.message;
      
      // Log the user object received from the API
      console.log('User object received from API:', data.user);

      if (authMode.value === 'login' || authMode.value === 'artist') {
        userStore.login(data.user, data.token);
        router.push('/');
      }

    } else {
      const errorText = await response.text();
      authError.value = errorText;
    }
  } catch (err) {
    authError.value = 'A network error occurred. Please try again.';
    console.error('Authentication failed:', err);
  } finally {
    isLoading.value = false;
  }
};
</script>

<style>
</style>
