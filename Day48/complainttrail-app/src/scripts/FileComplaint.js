import axios from "axios";

// Service to file a complaint
export const fileComplaint = async (complaintData) => {
    const url = "http://localhost:5062/api/Complaint/FileComplaint";

    // Create FormData object to handle multipart/form-data
    const formData = new FormData();
    formData.append("UserId", complaintData.userId);
    formData.append("OrganizationId", complaintData.organizationId);
    formData.append("CategoryId", complaintData.categoryId);
    formData.append("Description", complaintData.description);
    formData.append("Priority", complaintData.priority);
    if (complaintData.status !== undefined) formData.append("status", complaintData.status);
    if (complaintData.commentByUser) formData.append("CommentByUser", complaintData.commentByUser);

    // Add attachments if they exist
    if (complaintData.attachmentUrl && Array.isArray(complaintData.attachmentUrl)) {

        complaintData.attachmentUrl.forEach((file) => {
            formData.append(`AttachmentUrl`, file);
        });
    }


    formData.append("AcceptTermsAndConditions", complaintData.acceptTermsAndConditions);

    const token = sessionStorage.getItem("token");

    if (!token) {
        throw new Error("Authorization token not found in sessionStorage.");
    }

    try {
        const response = await axios.post(url, formData, {
            headers: {
                Authorization: `Bearer ${token}`, // Replace with your actual token
                "Content-Type": "multipart/form-data",
            },
        });
        return response.data; // Return API response data
    } catch (error) {
        console.error("Error filing the complaint:", error);
        throw error; // Propagate error to handle it in the caller function
    }
};
