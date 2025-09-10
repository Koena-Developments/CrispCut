import { defineStore } from 'pinia';
import { ref, computed } from 'vue';

export const useUserStore = defineStore('user', () => {
  // Reactive state for the user's login status and data
  const isLoggedIn = ref(false);
  const user = ref(null);
  const token = ref(null);

  // A computed property to check if the current user is an artist
  const isArtist = computed(() => {
    return user.value && user.value.role === 'Artist';
  });

  // Action to handle login
  const login = (userData, userToken) => {
    isLoggedIn.value = true;
    user.value = userData;
    token.value = userToken;
    
    // Store token and user data in localStorage for persistence
    localStorage.setItem('token', userToken);
    localStorage.setItem('user', JSON.stringify(userData));
  };

  // Action to handle logout
  const logout = () => {
    isLoggedIn.value = false;
    user.value = null;
    token.value = null;

    // Clear data from localStorage
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  };

  return { isLoggedIn, user, token, isArtist, login, logout };
});
