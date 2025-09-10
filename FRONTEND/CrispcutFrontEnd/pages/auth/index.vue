<template>
  <div class="min-h-screen flex items-center justify-center p-4 bg-slate-900">
    <!-- Main Card Container -->
    <div class="bg-gray-800 p-8 rounded-2xl shadow-2xl w-full max-w-lg border border-gray-700">
      <div class="flex flex-col items-center mb-6">
        <h1 class="text-4xl font-extrabold text-white">{{ isLogin ? 'Welcome Back!' : 'Join Us Today!' }}</h1>
        <p class="mt-2 text-gray-400 text-center">{{ isLogin ? 'Sign in to access your account.' : 'Create your account to get started.' }}</p>
      </div>

      <!-- Toggle between Login and Signup -->
      <div class="flex justify-center mb-6">
        <button
          @click="isLogin = true"
          :class="{ 'bg-blue-600 text-white shadow-lg': isLogin, 'bg-gray-700 text-gray-400': !isLogin }"
          class="px-6 py-2 rounded-l-full font-semibold transition-all duration-300 transform hover:scale-105"
        >
          Login
        </button>
        <button
          @click="isLogin = false"
          :class="{ 'bg-blue-600 text-white shadow-lg': !isLogin, 'bg-gray-700 text-gray-400': isLogin }"
          class="px-6 py-2 rounded-r-full font-semibold transition-all duration-300 transform hover:scale-105"
        >
          Sign Up
        </button>
      </div>

      <!-- Login Form -->
      <form v-if="isLogin" @submit.prevent="handleAuth" class="space-y-6">
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

      <!-- Signup Form -->
      <form v-else @submit.prevent="handleAuth" class="space-y-4">
        <!-- User Role Selection -->
        <div class="flex items-center space-x-4">
          <label class="inline-flex items-center text-gray-300">
            <input type="radio" v-model="isArtist" :value="false" class="form-radio text-blue-600" />
            <span class="ml-2">Standard User</span>
          </label>
          <label class="inline-flex items-center text-gray-300">
            <input type="radio" v-model="isArtist" :value="true" class="form-radio text-blue-600" />
            <span class="ml-2">Artist</span>
          </label>
        </div>

        <!-- Common Signup Fields -->
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

        <!-- Artist-Specific Fields -->
        <template v-if="isArtist">
          <div>
            <label for="category" class="block text-sm font-medium text-gray-300">Category</label>
            <select
              id="category"
              v-model="artistData.category"
              required
              class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            >
              <option disabled value="">Please select a category</option>
              <option value="Barber">Barber</option>
              <option value="MakeupArtist">MakeupArtist</option>
              <option value="TattooArtist">TattooArtist</option>
              <option value="Hairstylist">Hairstylist</option>
              <option value="Other">Other</option>
            </select>
          </div>
          <div>
            <label for="certificate" class="block text-sm font-medium text-gray-300">Certificate</label>
            <input
              id="certificate"
              v-model="artistData.certificate"
              type="text"
              required
              class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
              placeholder="Enter your certificate number or license ID"
            >
          </div>
          <div>
            <label for="address" class="block text-sm font-medium text-gray-300">Address</label>
            <input
              id="address"
              v-model="artistData.address"
              type="text"
              required
              autocomplete="street-address"
              class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
              placeholder="123 Main St, City, Country"
            >
          </div>
          <div>
            <label for="bio" class="block text-sm font-medium text-gray-300">Bio</label>
            <textarea
              id="bio"
              v-model="artistData.bio"
              rows="3"
              required
              class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
              placeholder="Tell us about your services and experience..."
            ></textarea>
          </div>
          <div>
            <label for="locationLat" class="block text-sm font-medium text-gray-300">Latitude</label>
            <input
              id="locationLat"
              v-model.number="artistData.locationLat"
              type="number"
              step="any"
              required
              class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
              placeholder="e.g., 40.7128"
            >
          </div>
          <div>
            <label for="locationLng" class="block text-sm font-medium text-gray-300">Longitude</label>
            <input
              id="locationLng"
              v-model.number="artistData.locationLng"
              type="number"
              step="any"
              required
              class="mt-1 block w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
              placeholder="e.g., -74.0060"
            >
          </div>
        </template>
        
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
      
      <!-- Error & Success Messages -->
      <p v-if="authError" class="mt-4 text-center text-red-400 font-medium">{{ authError }}</p>
      <p v-if="authSuccess" class="mt-4 text-center text-green-400 font-medium">{{ authSuccess }}</p>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useUserStore } from '@/stores/UserStore';

const isLogin = ref(true);
const isArtist = ref(false);
const authData = ref({
  email: '',
  password: '',
  firstName: '',
  lastName: '',
  phoneNumber: '',
});

const artistData = ref({
  category: '',
  certificate: '',
  address: '',
  bio: '',
  locationLat: 0,
  locationLng: 0,
  operatingHours: '',
});

const isLoading = ref(false);
const authError = ref('');
const authSuccess = ref('');

const router = useRouter();
const userStore = useUserStore();

const handleAuth = async () => {
  isLoading.value = true;
  authError.value = '';
  authSuccess.value = '';

  let endpoint = '';
  let payload = {};

  if (isLogin.value) {
    endpoint = 'http://localhost:5007/api/Auth/login';
    // FIX: Explicitly add the Role field to the login payload
    payload = {
      email: authData.value.email,
      password: authData.value.password,
      role: 'StandardUser',
    };
  } else {
    if (isArtist.value) {
      endpoint = 'http://localhost:5007/api/Artists/onboard';
      payload = {
        ...authData.value,
        ...artistData.value,
        role: 'Artist'
      };
    } else {
      endpoint = 'http://localhost:5007/api/Auth/register';
      payload = {
        ...authData.value,
        role: 'StandardUser',
        // Adding placeholder values for required fields not in this form
        bio: 'N/A',
        address: 'N/A',
        certificate: 'N/A',
      };
    }
  }

  try {
    const response = await fetch(endpoint, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(payload),
    });

    if (response.ok) {
      const data = await response.json();
      authSuccess.value = data.message;
      
      userStore.login(data.user, data.token);

      if (userStore.isArtist) {
        router.push('artistpages/');
      } else {
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
/* You can add custom styles here if needed, but Tailwind handles most of it. */
</style>
