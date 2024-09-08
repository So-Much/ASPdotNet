const confirmPassword = (password, confirmPassword) => {
    return password === confirmPassword ? null : "Password and Confirm Password do not match";
}
const emailRegular = (email) => {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email) ? null : "Email is invalid";
}
const passwordValidationError = (password) => {
    // Password is required by default
    if (!password) return 'Password is required';
    // Password must be at least 8 characters
    if (password.length < 8) return 'Password must be at least 8 characters';
    // Password must did not contain special characters unless !@#$%^&*()_+
    if (!/^[a-zA-Z0-9!@#$%^&*()_+]+$/.test(password)) return 'Password must did not contain special characters unless !@#$%^&*()_+';
    return null;
}
export {
    confirmPassword,
    emailRegular,
    passwordValidationError
}