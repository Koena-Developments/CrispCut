import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useUserStore = defineStore('user', () => {
  // Reactive state for the user's login status and data
  const isLoggedIn = ref(false);
  const user = ref(null);
  const token = ref(null);

  // Action to handle login
  const login = (userData, userToken) => {
    isLoggedIn.value = true;
    user.value = userData;
    token.value = userToken;
    
    localStorage.setItem('token', userToken);
    localStorage.setItem('user', JSON.stringify(userData));
  };

  // Action to handle logout
  const logout = () => {
    isLoggedIn.value = false;
    user.value = null;
    token.value = null;

    localStorage.removeItem('token');
    localStorage.removeItem('user');
  };

  return { isLoggedIn, user, token, login, logout };
});
