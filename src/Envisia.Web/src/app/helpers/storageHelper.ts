export const addToStorage = (key: string, value: string): void => {
    localStorage.setItem(key, value)
};

export const getFromStorage = (key: string): any => {
    return localStorage.getItem(key);
};

export const removeFromStorage = (key: string): void => {
    sessionStorage.removeItem(key);
};
